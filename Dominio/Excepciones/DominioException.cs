namespace SistemaApiRest.Dominio.Excepciones
{

    /// Excepción base para errores originados en el dominio (reglas de negocio).
    /// Al ser una excepción propia del dominio, no depende de ninguna capa externa.

    public class DominioException : Exception
    {
        public DominioException(string mensaje) : base(mensaje)
        {
        }
    }
}
