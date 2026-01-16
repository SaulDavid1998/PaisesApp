using System.Text.Json;

namespace MVC.Models
{
    public  static class SessionExtension
    {
        // Método de extensión para guardar objetos en la sesión
        public static void SetObject(this ISession session, string clave, object valor)
        {
            // Convierte el objeto a una cadena JSON
            string json = JsonSerializer.Serialize(valor);

            // Guarda la cadena JSON en la sesión usando la clave proporcionada
            session.SetString(clave, json);
        }

        // Método de extensión para obtener un objeto desde la sesión
        public static T GetObjectFromJson<T>(this ISession session, string clave)
        {
            // Intenta obtener la cadena JSON guardada con la clave
            var valor = session.GetString(clave);

            // Si no hay nada guardado, devuelve el valor por defecto del tipo
            if (string.IsNullOrEmpty(valor))
            {
                return default(T);
            }
            else
            {
                // Si hay algo, lo convierte de JSON a objeto y lo devuelve
                return JsonSerializer.Deserialize<T>(valor);
            }
        }
        //public static void RemoveObject(this ISession session, string clave)
        //{
        //    session.Remove(clave);
        //}

        public static int ObtenerContador(this ISession session, string clave)
        {
            return session.GetInt32(clave) ?? 0;
        }
    }
}
