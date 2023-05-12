using System;
using System.ComponentModel;
using System.Linq;

namespace AlimentacaoInfantil.Enums
{
    public enum EnumTipoUsuario
    {
        [Description("Pai/Mãe")]
        PAIS = 0,

        [Description("Nutricionista")]
        NUTRICIONISTA = 1
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this EnumTipoUsuario value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                     .FirstOrDefault() as DescriptionAttribute;

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}