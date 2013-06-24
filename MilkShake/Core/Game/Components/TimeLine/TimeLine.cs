using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game.Components.Animation;
using MilkShakeFramework.Tools.Utils;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using XNATweener;
using MilkShakeFramework.Tools.Tween;

namespace MilkShakeFramework.Core.Game.Components.TimeLine
{
    public delegate void TimeLineTaskComplete();

    public abstract class TimeLineTask
    {
        public TimeLineTaskComplete OnComplete;

        public abstract void Run();

        public void Complete()
        {
            if (OnComplete != null) OnComplete();
            else throw new Exception("On Complete == null");
        }
    }

    public class DelayTask : TimeLineTask
    {
        public float Time { get; private set; }

        public DelayTask(float time)
        {
            Time = time;
        }

        public override void Run()
        {
            new Wait(Time, Complete);
        }
    }

    public class TweenTask : TimeLineTask
    {
        public Tweener Tweener { get; private set; }
        public TweenPositionUpdate UpdateCallback { get; private set; }

        public TweenTask(Tweener tweener, TweenPositionUpdate updateCallback)
        {
            Tweener = tweener;
            UpdateCallback = updateCallback;
        }

        public override void Run()
        {
            TweenerManager.AddTween(Tweener, UpdateCallback, Complete);
        }
    }

    public class CustomTask : TimeLineTask
    {
        private BasicEvent _customAction;

        public CustomTask(BasicEvent basicEvent)
        {
            _customAction = basicEvent;
        }

        public override void Run()
        {
            _customAction();
            Complete();
        }
    }

    public class PlayAnimationTask : TimeLineTask
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public string AnimationKey{ get; set; }

        public PlayAnimationTask(AnimatedSprite animatedSprite, string animationKey)
        {
            AnimatedSprite = animatedSprite;
            AnimationKey = animationKey;
        }

        public override void Run()
        {
            AnimatedSprite.OnAnimationComplete += OnAnimationComplete;
            AnimatedSprite.SetAnimation(AnimationKey, PlayMode.PlayAndPause);
        }

        private void OnAnimationComplete(string key)
        {
            if (key == AnimationKey)
            {
                // Remove listener
                AnimatedSprite.OnAnimationComplete -= OnAnimationComplete;

                Complete();
            }
        }
    }

    public class TimeLine : GameEntity
    {
        public bool Proccessing { get; private set; }
        public TimeLineTask CurrentTask { get; private set; }
        public Queue<TimeLineTask> Tasks { get; private set; }

        public TimeLine()
        {
            Tasks = new Queue<TimeLineTask>();
        }

        public void AddTask(TimeLineTask task)
        {
            Tasks.Enqueue(task);
        }

        public void AddTask(BasicEvent customAction)
        {
            AddTask(new CustomTask(customAction));
        }

        public void Start()
        {
            CheckQueue();
        }

        private void CheckQueue()
        {
            if (Tasks.Count > 0)
            {
                Proccessing = true;
                ProccessTask();
            }
            else
            {
                Proccessing = false;
                CurrentTask = null;
            }
        }

        private void ProccessTask()
        {
            CurrentTask = Tasks.Dequeue();

            CurrentTask.OnComplete += CheckQueue;

            CurrentTask.Run();
        }
    }

}
