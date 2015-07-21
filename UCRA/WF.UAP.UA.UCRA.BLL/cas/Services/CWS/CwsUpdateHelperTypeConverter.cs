namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// The cws update helper type converter.
    /// </summary>
    public class CwsUpdateHelperTypeConverter : TypeConverter
    {
        #region Public Methods and Operators

        /// <summary>
        /// The can convert from.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="sourceType">
        /// The source type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// The can convert to.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="destinationType">
        /// The destination type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// The convert from.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] v = ((string)value).Split(new[] { ':', ':' });
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// The convert to.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="destinationType">
        /// The destination type.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}