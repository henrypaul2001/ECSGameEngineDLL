using EngineDLL.Managers;
using EngineDLL.OBJLoader;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Diagnostics;

namespace EngineDLL.Components
{
    public class ComponentAudio : IComponent
    {
        Vector3 sourcePosition;
        int audioBuffer;
        int audioSource;

        public ComponentAudio(string filename, bool looping, bool instantPlay)
        {
            // Setup Audio Source from the Audio Buffer
            audioBuffer = ResourceManager.LoadAudio(filename);
            audioSource = AL.GenSource();
            AL.Source(audioSource, ALSourcei.Buffer, audioBuffer);
            AL.Source(audioSource, ALSourceb.Looping, looping);
            AL.Source(audioSource, ALSource3f.Position, ref sourcePosition);

            if (instantPlay) { Play(); }
        }

        public void Play()
        {
            AL.SourcePlay(audioSource);
        }

        public void Stop()
        {
            AL.SourceStop(audioSource);
        }

        public void Close()
        {
            Stop();
            AL.DeleteSource(audioSource);
        }
        public void SetPosition(Vector3 emitterPosition)
        {
            sourcePosition = emitterPosition;
            AL.Source(audioSource, ALSource3f.Position, ref sourcePosition);
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_AUDIO; }
        }
    }
}
