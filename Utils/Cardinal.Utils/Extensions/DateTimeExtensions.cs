using Cardinal.Utils;
using System;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Data Inicial de um DateTime.
        /// </summary>
        private static DateTime InitialDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Extensão que traz a diferença da data atual à data inicial válida.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="field">Tipo de campo à ser retornado</param>
        /// <returns>Diferença entre as datas baseando-se no tipo de campo solicitado</returns>
        public static long Elapsed(this DateTime source, DateField field)
        {
            return source.Diference(InitialDate, field);
        }

        /// <summary>
        /// Extensão que verifica se uma data está entre duas datas específicas.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="initialDate">Data inicial do período</param>
        /// <param name="finalDate">Data final do período</param>
        /// <returns>Verdadeiro caso a data esteja dentro do período e falso caso contrário</returns>
        public static bool IsBetween(this DateTime source, DateTime initialDate, DateTime finalDate)
        {
            return source >= initialDate && source <= finalDate;
        }

        /// <summary>
        /// Extensão que traz a diferença da data atual à uma data informada.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="date">Data à ser comparada</param>
        /// <param name="field">Tipo de campo à ser retornado</param>
        /// <returns>Diferença entre as datas baseando-se no tipo de campo solicitado</returns>
        public static long Diference(this DateTime source, DateTime date, DateField field)
        {
            var timeSpan = source - date;

            var result = GetResult(timeSpan, field);

            if (result < 0)
            {
                result *= (-1);
            }
            return result;
        }

        /// <summary>
        /// Método que retorna o resultado da diferença entre as datas.
        /// </summary>
        /// <param name="timeSpan">TimeSpan de diferenciação entre datas</param>
        /// <param name="field">Tipo de campo desejado</param>
        /// <returns>Contagem de diferença entre as datas com base no tipo de campo informado</returns>
        private static long GetResult(TimeSpan timeSpan, DateField field)
        {
            switch (field)
            {
                case DateField.Days:
                    return timeSpan.Days;
                case DateField.Hours:
                    return timeSpan.Hours;
                case DateField.Minutes:
                    return timeSpan.Minutes;
                case DateField.Seconds:
                    return timeSpan.Seconds;
                case DateField.Milissecounds:
                    return timeSpan.Milliseconds;
                default:
                    return timeSpan.Ticks;
            }
        }
    }
}
