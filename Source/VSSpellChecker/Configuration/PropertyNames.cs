﻿//===============================================================================================================
// System  : Visual Studio Spell Checker Package
// File    : PropertyNames.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 02/01/2015
// Note    : Copyright 2015, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the class containing the configuration property name constants and some helper methods
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/VSSpellChecker
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
//===============================================================================================================
// 02/01/2015  EFW  Refactored configuration settings
//===============================================================================================================

namespace VisualStudio.SpellChecker.Configuration
{
    /// <summary>
    /// This class contains the configuration property name constants and some helper methods
    /// </summary>
    internal static class PropertyNames
    {
        #region Property name constants
        //=====================================================================

        /// <summary>
        /// Default language
        /// </summary>
        public const string DefaultLanguage = "DefaultLanguage";

        /// <summary>
        /// Spell check as you type
        /// </summary>
        public const string SpellCheckAsYouType = "SpellCheckAsYouType";

        /// <summary>
        /// Ignore words with digits
        /// </summary>
        public const string IgnoreWordsWithDigits = "IgnoreWordsWithDigits";

        /// <summary>
        /// Ignore words in all uppercase
        /// </summary>
        public const string IgnoreWordsInAllUppercase = "IgnoreWordsInAllUppercase";

        /// <summary>
        /// Ignore format specifiers
        /// </summary>
        public const string IgnoreFormatSpecifiers = "IgnoreFormatSpecifiers";

        /// <summary>
        /// Ignore filenames and e-mail addresses
        /// </summary>
        public const string IgnoreFilenamesAndEMailAddresses = "IgnoreFilenamesAndEMailAddresses";

        /// <summary>
        /// Ignore XML elements in text
        /// </summary>
        public const string IgnoreXmlElementsInText = "IgnoreXmlElementsInText";

        /// <summary>
        /// Treat underscore as separator
        /// </summary>
        public const string TreatUnderscoreAsSeparator = "TreatUnderscoreAsSeparator";

        /// <summary>
        /// Ignore character class
        /// </summary>
        public const string IgnoreCharacterClass = "IgnoreCharacterClass";

        /// <summary>
        /// Exclude by filename extension
        /// </summary>
        public const string ExcludeByFilenameExtension = "ExcludeByFilenameExtension";

        /// <summary>
        /// C# - Ignore XML doc comments
        /// </summary>
        public const string CSharpOptionsIgnoreXmlDocComments = "CSharpOptions.IgnoreXmlDocComments";

        /// <summary>
        /// C# - Ignore delimited comments
        /// </summary>
        public const string CSharpOptionsIgnoreDelimitedComments = "CSharpOptions.IgnoreDelimitedComments";

        /// <summary>
        /// C# - Ignore standard single line comments
        /// </summary>
        public const string CSharpOptionsIgnoreStandardSingleLineComments = "CSharpOptions.IgnoreStandardSingleLineComments";

        /// <summary>
        /// C# - Ignore quadruple slash comments
        /// </summary>
        public const string CSharpOptionsIgnoreQuadrupleSlashComments = "CSharpOptions.IgnoreQuadrupleSlashComments";

        /// <summary>
        /// C# - Ignore normal strings
        /// </summary>
        public const string CSharpOptionsIgnoreNormalStrings = "CSharpOptions.IgnoreNormalStrings";

        /// <summary>
        /// C# - Ignore verbatim strings
        /// </summary>
        public const string CSharpOptionsIgnoreVerbatimStrings = "CSharpOptions.IgnoreVerbatimStrings";

        /// <summary>
        /// Inherit ignored words
        /// </summary>
        public const string InheritIgnoredWords = "InheritIgnoredWords";

        /// <summary>
        /// Ignored words
        /// </summary>
        public const string IgnoredWords = "IgnoredWords";

        /// <summary>
        /// Ignored words item
        /// </summary>
        public const string IgnoredWordsItem = "Ignore";

        /// <summary>
        /// Inherit XML settings
        /// </summary>
        public const string InheritXmlSettings = "InheritXmlSettings";

        /// <summary>
        /// Ignored XML elements
        /// </summary>
        public const string IgnoredXmlElements = "IgnoredXmlElements";

        /// <summary>
        /// Ignored XML elements item
        /// </summary>
        public const string IgnoredXmlElementsItem = "Ignore";

        /// <summary>
        /// Spell checked XML attributes
        /// </summary>
        public const string SpellCheckedXmlAttributes = "SpellCheckedXmlAttributes";

        /// <summary>
        /// Spell checked XML attributes item
        /// </summary>
        public const string SpellCheckedXmlAttributesItem = "SpellCheck";

        #endregion
    }
}
