using System;
using System.Linq;

namespace Cardinal.Utils
{
    /// <summary>
    /// Métodos de auxílio matemático.
    /// </summary>
    public static class CardinalMath
    {
        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static decimal Sum(params decimal[] values)
        {
            var result = 0M;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static int Sum(params int[] values)
        {
            var result = 0;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static double Sum(params double[] values)
        {
            var result = 0D;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que faz a soma de valores.
        /// </summary>
        /// <param name="values">Valores à serem somados.</param>
        /// <returns>Resultado da soma dos valores.</returns>
        public static float Sum(params float[] values)
        {
            var result = 0F;

            foreach (var value in values)
            {
                result += value;
            }
            return result;
        }

        /// <summary>
        /// Método que retorna o maior valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static decimal Max(params decimal[] values)
        {
            return values != null ? values.Max() : 0M;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static int Max(params int[] values)
        {
            return values != null ? values.Max() : 0;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static double Max(params double[] values)
        {
            return values != null ? values.Max() : 0D;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Maior valor dentre os valores informados.</returns>
        public static float Max(params float[] values)
        {
            return values != null ? values.Max() : 0F;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static decimal Min(params decimal[] values)
        {
            return values != null ? values.Min() : 0M;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static double Min(params int[] values)
        {
            return values != null ? values.Min() : 0;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static double Min(params double[] values)
        {
            return values != null ? values.Min() : 0D;
        }

        /// <summary>
        /// Método que retorna o menor valor entre os valores
        /// informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Menor valor dentre os valores informados.</returns>
        public static float Min(params float[] values)
        {
            return values != null ? values.Min() : 0F;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static decimal Media(params decimal[] values)
        {
            return values != null ? values.Average() : 0M;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static double Media(params int[] values)
        {
            return values != null ? values.Average() : 0;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static double Media(params double[] values)
        {
            return values != null ? values.Average() : 0D;
        }

        /// <summary>
        /// Método que retorna o valor médio dentre os 
        /// valores informados.
        /// </summary>
        /// <param name="values">Valores para a verificação.</param>
        /// <returns>Valor médio entre os valores informados.</returns>
        public static float Media(params float[] values)
        {
            return values != null ? values.Average() : 0F;
        }
    }
}
