﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 11.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace JGen.Templates
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonFloatReaderTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public partial class JsonFloatReaderTemplate : JsonFloatReaderTemplateBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.IO;\r\n\r\nnamespace JGen.Readers\r\n{\r\n\tpublic static clas" +
                    "s ");
            
            #line 11 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonFloatReaderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ReaderName));
            
            #line default
            #line hidden
            this.Write("\r\n\t{\r\n\t\tpublic static ");
            
            #line 13 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonFloatReaderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Type));
            
            #line default
            #line hidden
            this.Write(" Read(TextReader _textReader)\r\n\t\t{\r\n\t\t\tbool negative = false;\r\n\t\t\tif (_textReader" +
                    ".Peek() == \'-\')\r\n\t\t\t{\r\n\t\t\t\tnegative = true;\r\n\t\t\t\t_textReader.Read();\r\n\t\t\t\tif (_t" +
                    "extReader.Peek() < 0)\r\n\t\t\t\t\tthrow new JsonException(\"Invalid JSON numeric litera" +
                    "l; extra negation\");\r\n\t\t\t}\r\n\r\n\t\t\tint c;\r\n\t\t\tdecimal val = 0;\r\n\t\t\tint x = 0;\r\n\t\t\t" +
                    "bool zeroStart = _textReader.Peek() == \'0\';\r\n\t\t\tfor (; ; x++)\r\n\t\t\t{\r\n\t\t\t\tc = _te" +
                    "xtReader.Peek();\r\n\t\t\t\tif (c < \'0\' || \'9\' < c)\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tval = val * 10 +" +
                    " (c - \'0\');\r\n\t\t\t\t_textReader.Read();\r\n\t\t\t\tif (zeroStart && x == 1 && c == \'0\')\r\n" +
                    "\t\t\t\t\tthrow new JsonException(\"leading multiple zeros are not allowed\");\r\n\t\t\t}\r\n\r" +
                    "\n\t\t\t// fraction\r\n\r\n\t\t\tbool hasFrac = false;\r\n\t\t\tdecimal frac = 0;\r\n\t\t\tint fdigit" +
                    "s = 0;\r\n\t\t\tif (_textReader.Peek() == \'.\')\r\n\t\t\t{\r\n\t\t\t\thasFrac = true;\r\n\t\t\t\t_textR" +
                    "eader.Read();\r\n\t\t\t\tif (_textReader.Peek() < 0)\r\n\t\t\t\t\tthrow new JsonException(\"In" +
                    "valid JSON numeric literal; extra dot\");\r\n\t\t\t\tdecimal d = 10;\r\n\t\t\t\twhile (true)\r" +
                    "\n\t\t\t\t{\r\n\t\t\t\t\tc = _textReader.Peek();\r\n\t\t\t\t\tif (c < \'0\' || \'9\' < c)\r\n\t\t\t\t\t\tbreak;" +
                    "\r\n\t\t\t\t\t_textReader.Read();\r\n\t\t\t\t\tfrac += (c - \'0\') / d;\r\n\t\t\t\t\td *= 10;\r\n\t\t\t\t\tfdi" +
                    "gits++;\r\n\t\t\t\t}\r\n\t\t\t\tif (fdigits == 0)\r\n\t\t\t\t\tthrow new JsonException(\"Invalid JSO" +
                    "N numeric literal; extra dot\");\r\n\t\t\t}\r\n\t\t\tfrac = Decimal.Round(frac, fdigits);\r\n" +
                    "\r\n\t\t\tc = _textReader.Peek();\r\n\t\t\tif (c != \'e\' && c != \'E\')\r\n\t\t\t{\r\n\t\t\t\tif (!hasFr" +
                    "ac)\r\n\t\t\t\t{\r\n\r\n\t\t\t\t\t\treturn (double)(negative ? -val : val);\t\t\t\t\t\r\n\t\t\t\t}\r\n\t\t\t\tvar" +
                    " v = val + frac;\r\n\t\t\t\treturn (double)(negative ? -v : v);\r\n\t\t\t}\r\n\r\n\t\t\t// exponen" +
                    "t\r\n\t\t\t_textReader.Read();\r\n\r\n\t\t\tint exp = 0;\r\n\t\t\tif (_textReader.Peek() < 0)\r\n\t\t" +
                    "\t\tthrow new JsonException(\"Invalid JSON numeric literal; incomplete exponent\");\r" +
                    "\n\r\n\t\t\tbool negexp = false;\r\n\t\t\tc = _textReader.Peek();\r\n\t\t\tif (c == \'-\')\r\n\t\t\t{\r\n" +
                    "\t\t\t\t_textReader.Read();\r\n\t\t\t\tnegexp = true;\r\n\t\t\t}\r\n\t\t\telse if (c == \'+\')\r\n\t\t\t\t_t" +
                    "extReader.Read();\r\n\r\n\t\t\tif (_textReader.Peek() < 0)\r\n\t\t\t\tthrow new ArgumentExcep" +
                    "tion(\"Invalid JSON numeric literal; incomplete exponent\");\r\n\t\t\twhile (true)\r\n\t\t\t" +
                    "{\r\n\t\t\t\tc = _textReader.Peek();\r\n\t\t\t\tif (c < \'0\' || \'9\' < c)\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tex" +
                    "p = exp * 10 + (c - \'0\');\r\n\t\t\t\t_textReader.Read();\r\n\t\t\t}\r\n\r\n\t\t\tif (negexp)\r\n\t\t\t\t" +
                    "return (");
            
            #line 107 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonFloatReaderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Type));
            
            #line default
            #line hidden
            this.Write(")(val + frac) / Math.Pow(10, exp);\r\n\t\t\telse\r\n\t\t\t\treturn (");
            
            #line 109 "c:\users\grigoryp\documents\visual studio 2012\Projects\JGen\JGen\Templates\JsonFloatReaderTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Type));
            
            #line default
            #line hidden
            this.Write(")(val + frac) * Math.Pow(10, exp);\r\n\t\t}\r\n\t}\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "11.0.0.0")]
    public class JsonFloatReaderTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
