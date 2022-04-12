using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SeriousPhysX
{
    public class ModuleVesselDetonation : PartModule
    {
        [UI_FloatRange(minValue = 0f, maxValue = 1f, stepIncrement =0.1f)]
        [KSPField(guiActive = true, guiActiveEditor = true, guiName = "Countdown", guiFormat = "P0", isPersistant = true)]
        float seconds = 0.9f;
        FXGroup alarmSound;
        //IEnumerator co;
        bool detonating = false;
        public override void OnStart(PartModule.StartState state)
        {
            SoundManager.LoadSound("SeriousPhysX/Sounds/Alarm", "Alarm");
            alarmSound = new FXGroup("heatAlarm");
            SoundManager.CreateFXSound(null, alarmSound, "Alarm", false);
            base.OnStart(state);
        }
        //public override void OnUpdate()
        //{
        //if (detonating && !alarmSound.audio.isPlaying)
        //    alarmSound.audio.Play();
        //if (!detonating && alarmSound.audio.isPlaying)
        //    alarmSound.audio.Stop();
        //}
        [KSPAction("Toggle Detonation")]
        public void ToggleAction()
        {
            ToggleDetonate();
        }
        [KSPEvent(guiActive = true, guiName = "Detonate")]
        public void ToggleDetonate()
        {
            detonating = !detonating;
            Events["ToggleDetonate"].guiName = (detonating ? "Cancel" : "Detonate");
            if (detonating)
            {
                alarmSound.audio.Play();
                StartCoroutine("Countdown");
            }
            else
            {
                alarmSound.audio.Stop();
                StopCoroutine("Countdown");
            }
        }
        public IEnumerator Countdown()
        {
            while (true)
            {
                Vessel vessel = FlightGlobals.ActiveVessel;
                yield return new WaitForSeconds(seconds*10);
                List<Part> parts = vessel.parts;
                int threshold = (int)(parts.Count * 0.2);
                System.Random rnd = new System.Random();
                while (parts.Count > threshold)
                {
                    parts[rnd.Next(1, parts.Count)].explode();
                    parts = vessel.parts;
                }
                ToggleDetonate();
            }
        }
    }
}
