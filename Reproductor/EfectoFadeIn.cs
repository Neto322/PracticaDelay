using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;
namespace Reproductor
{
    class EfectoFadeIn : ISampleProvider
    {
        private ISampleProvider fuente;

        private int muestrasLeidas = 0;

        private float segundosTranscurridos = 0;

        private float duracion;


        private float inicio;

        float factordeescala;

        public EfectoFadeIn(ISampleProvider fuente,float duracion)
        {
            this.fuente = fuente;
            this.inicio = duracion;
       
        }
        public WaveFormat WaveFormat
        {
            get
            {
                return fuente.WaveFormat;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int read = fuente.Read(buffer, offset, count);

            //Aplicar el efecto

            muestrasLeidas += read;

            segundosTranscurridos = (float)muestrasLeidas / (float)fuente.WaveFormat.SampleRate / (float)fuente.WaveFormat.Channels;

            if(segundosTranscurridos <= inicio)
            {
                //Aplicar el efecto
                float factorescala = segundosTranscurridos / inicio;
                for (int i=0; i<read; i++)
                {
                    buffer[i + offset] *= factorescala;
                }

            }
            Console.WriteLine(factordeescala);
            return read;
        }
    }
}
