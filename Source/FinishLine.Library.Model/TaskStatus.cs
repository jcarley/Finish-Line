using System;
using System.Linq;
using System.Collections.Generic;

namespace FinishLine.Library.Model
{
    // Active, Complete, Done, InDevelopment, InTesting, New, Planned, Resubmitted, Closed, Fixed, InProgress, NeedMoreInformation, Open, 
    // ReadyForTest, Resolved, Verified

    public enum State
    {
        Active,
        Complete,
        Done,
        InDevelopment,
        InTesting,
        New,
        Planned,
        Resubmitted,
        Closed,
        Fixed,
        InProgress,
        NeedMoreInformation,
        Open,
        ReadyForTest,
        Resolved,
        Verified
    }

    public static class WorkItemStateFactory
    {
        public static WorkItemState GetState(State state)
        {
            string stateName = Enum.GetName(typeof(State), state);

            string stateTypeName = string.Format("FinishLine.Library.Model.{0}State", stateName);

            Type workItemStateType = Type.GetType(stateTypeName);

            if (workItemStateType == null)
                throw new NullReferenceException(string.Format("Unable to determine type for state: {0}", stateTypeName));

            WorkItemState newState = Activator.CreateInstance(workItemStateType) as WorkItemState;

            return newState;
        }

    }

    public abstract class WorkItemState
    {
        public abstract State Value { get; }

        public abstract IList<State> NextAvailableStates { get; }
        
        public virtual bool IsAllow(State state)
        {
            return NextAvailableStates.Any(s => s == state);
        }
    }

    public class ActiveState : WorkItemState
    {
        public override State Value
        {
            get { return State.Active; }
        }

        public override IList<State> NextAvailableStates
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class CompleteState : WorkItemState
    {
        public override State Value
        {
            get { return State.Complete; }
        }

        public override IList<State> NextAvailableStates
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class NewState : WorkItemState
    {
        public override State Value
        {
            get { return State.New; }
        }

        public override IList<State> NextAvailableStates
        {
            get { throw new NotImplementedException(); }
        }
    }
}
