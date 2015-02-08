﻿//===============================================================================================================
// System  : Visual Studio Spell Checker Package
// File    : SpellConfigurationEditorControl.xaml.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 02/08/2015
// Note    : Copyright 2015, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a user control used to edit spell checker configuration settings files
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/VSSpellChecker
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/14/2013  EFW  Created the code
// 06/09/2014  EFW  Reworked to use a tree view and user controls for the various configuration categories
// 02/01/2015  EFW  Refactored the configuration settings to allow for solution and project specific settings
// 02/07/2015  EFW  Moved the code into a user control hosted within a Visual Studio editor pane
//===============================================================================================================

using System;
using System.Diagnostics;
using System.Web;
using System.Windows;
using System.Windows.Controls;

using PackageResources = VisualStudio.SpellChecker.Properties.Resources;

using VisualStudio.SpellChecker.Configuration;
using VisualStudio.SpellChecker.Editors.Pages;

namespace VisualStudio.SpellChecker.Editors
{
    /// <summary>
    /// This user control is used to edit spell checker configuration settings files
    /// </summary>
    /// <remarks>Since all settings files are XML files, this can be used to edit the global configuration as
    /// well as any project-specific settings files.</remarks>
    public partial class SpellingConfigurationEditorControl : UserControl
    {
        #region Private data members
        //=====================================================================

        private SpellingConfigurationFile configFile;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property returns the filename
        /// </summary>
        public string Filename
        {
            get
            {
                return (configFile == null) ? String.Empty : configFile.Filename;
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This is raised to let the parent know that the configuration changed
        /// </summary>
        public event EventHandler ConfigurationChanged;

        /// <summary>
        /// This is called to raise the <see cref="ConfigurationChanged"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void OnConfigurationChanged(object sender, EventArgs e)
        {
            var handler = ConfigurationChanged;

            if(handler != null)
                handler(sender, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public SpellingConfigurationEditorControl()
        {
            ISpellCheckerConfiguration page;
            TreeViewItem node;

            InitializeComponent();

            // The property pages will be listed in this order
            Type[] propertyPages = new[] {
                typeof(GeneralSettingsUserControl),
                typeof(UserDictionaryUserControl),
                typeof(IgnoredWordsUserControl),
                typeof(XmlFilesUserControl),
                typeof(CSharpOptionsUserControl)
            };

            try
            {
                tvPages.BeginInit();

                // Create the property pages
                foreach(Type pageType in propertyPages)
                {
                    page = (ISpellCheckerConfiguration)Activator.CreateInstance(pageType);
                    page.Control.Visibility = Visibility.Collapsed;
                    page.ConfigurationChanged += OnConfigurationChanged;

                    node = new TreeViewItem();
                    node.Header = page.Title;
                    node.Name = pageType.Name;
                    node.Tag = page;

                    tvPages.Items.Add(node);
                    pnlPages.Children.Add(page.Control);
                }
            }
            finally
            {
                tvPages.EndInit();

                if(tvPages.Items.Count != 0)
                    ((TreeViewItem)tvPages.Items[0]).IsSelected = true;
            }
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// This is used to load the configuration file to edit
        /// </summary>
        /// <param name="configurationFile">The configuration filename</param>
        public void LoadConfiguration(string configurationFile)
        {
            configFile = new SpellingConfigurationFile(configurationFile, null);

            if(configFile.ConfigurationType == ConfigurationType.Global)
                lblFilename.Text = "Global spell checker configuration";
            else
                lblFilename.Text = configurationFile;

            foreach(TreeViewItem item in tvPages.Items)
                ((ISpellCheckerConfiguration)item.Tag).LoadConfiguration(configFile);
        }

        /// <summary>
        /// Save changes to the configuration
        /// </summary>
        /// <param name="configurationFile">The configuration filename</param>
        public void SaveConfiguration(string configurationFile)
        {
            configFile.Filename = configurationFile;

            if(configFile.ConfigurationType == ConfigurationType.Global)
                lblFilename.Text = "Global spell checker configuration";
            else
                lblFilename.Text = configurationFile;

            foreach(TreeViewItem item in tvPages.Items)
            {
                ISpellCheckerConfiguration page = (ISpellCheckerConfiguration)item.Tag;
                page.SaveConfiguration(configFile);
            }

            if(configFile.Save())
            {
                // If it's the global configuration, load the new settings
                if(configFile.ConfigurationType == ConfigurationType.Global)
                    SpellCheckerConfiguration.GlobalConfiguration.Load(configFile.Filename);
            }
            else
                MessageBox.Show("Unable to save spell checking configuration", PackageResources.PackageTitle,
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// View the project website
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void lnkProjectSite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(lnkProjectSite.NavigateUri.AbsoluteUri);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to navigate to website.  Reason: " + ex.Message,
                    PackageResources.PackageTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// View help for the selected property category
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)tvPages.SelectedItem;

            if(item != null)
            {
                ISpellCheckerConfiguration page = (ISpellCheckerConfiguration)item.Tag;

                try
                {
                    string targetUrl = lnkProjectSite.NavigateUri.AbsoluteUri + "/wiki/" +
                        HttpUtility.UrlEncode(page.HelpUrl);

                    Process.Start(targetUrl);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Unable to navigate to website.  Reason: " + ex.Message,
                        PackageResources.PackageTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        /// <summary>
        /// Reset the configuration to its default settings excluding the user dictionary
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to reset the configuration to its default settings " +
              "(excluding the user dictionary)?", PackageResources.PackageTitle, MessageBoxButton.YesNo,
              MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                // Pass a dummy filename to create a new configuration and then set the filename so that
                // the pages know the type of configuration file.
                var newConfigFile = new SpellingConfigurationFile("__ResetTemp__", new SpellCheckerConfiguration());
                newConfigFile.Filename = configFile.Filename;

                foreach(TreeViewItem item in tvPages.Items)
                    ((ISpellCheckerConfiguration)item.Tag).LoadConfiguration(newConfigFile);
            }

            this.OnConfigurationChanged(sender, e);
        }

        /// <summary>
        /// Change the displayed property page based on the selected tree view item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvPages_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            foreach(TreeViewItem item in tvPages.Items)
            {
                ISpellCheckerConfiguration page = (ISpellCheckerConfiguration)item.Tag;

                if(item.IsSelected)
                    page.Control.Visibility = Visibility.Visible;
                else
                    page.Control.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
