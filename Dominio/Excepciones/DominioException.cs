namespace SistemaApiRest.Dominio.Excepciones
{
    public class DominioException : Exception
    {
        public DominioException(string mensaje) : base(mensaje) { }
    }
}