using Autofac;
using Soft.Example.Test.Autofac._1_GettingStarted.Implementation;
using Soft.Example.Test.Autofac._1_GettingStarted.Interfaces;

namespace Soft.Example.Test.Autofac._1_GettingStarted
{
    public class Program
    {
        private static IContainer Container { get; set; }

        private static void Main(string[] args)
        {
            //Creamos el generador
            var builder = new ContainerBuilder();

            //Usualmente se expone el tipo via la interfaz
            builder.RegisterType<ConsoleOutput>().As<IOutput>().SingleInstance();

            //Sin embargo si se quiere Ambos servicios (no comun)
            //se hace asi  builder.RegisterType<SomeType>().AsSelf().As<IService>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();

            Container = builder.Build();

            //Este metodo es donde se hara uso 
            //de la inyeccion de dependendica
            WriteDate();
            WriteDate();
        }

        public static void WriteDate()
        {
            //Crea el alcanse para resolver el IDateWriter,
            //luego se dispose en el entorno
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }

        /*
         * Cuando se corre el programa hace:
         * El metodo WriteDate pregunta al Autofac por un IDateWriter
         * Autofac ve que IDateWriter esta con TodayWriter entonces crea un nuevo objeto TodayWriter
         * Autofac ve que TodayWriter esta mapeado con un IOutput en el constructor
         * Autofac ve que IOutput esta mapeado con ConsoleOutput entonces crea una nueva instancia de  ConsoleOutput
         * Autofac usa la nueva instanca de ConsoleOutput para terminar de crear el contructor de TodayWriter
         * Autofac retorna una contruccion completa para “WriteDate” para ser consumido
          */

    }
}