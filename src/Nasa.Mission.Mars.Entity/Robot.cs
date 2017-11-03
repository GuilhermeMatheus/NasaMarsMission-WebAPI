using Nasa.Mission.Mars.Entity.ModelConstraints;
using Nasa.Mission.Mars.Entity.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.Mission.Mars.Entity
{
    public class Robot : IConstrainable<Robot>
    {
        private Position _position;
        private Direction _direction;
        private JourneyStatus _journeyStatus;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Position Position
        {
            get { return _position; }
            private set
            {
                ConstraintValidator.ThrowIfInvalidState(value);
                _position = value;
            }
        }
        public Direction Direction
        {
            get { return _direction; }
            private set
            {
                ConstraintValidator.ThrowIfInvalidState(value);
                _direction = value;
            }
        }
        public JourneyStatus JourneyStatus
        {
            get { return _journeyStatus; }
            private set
            {
                ConstraintValidator.ThrowIfInvalidState(value);
                _journeyStatus = value;
            }
        }
        public ConstraintValidator<Robot> ConstraintValidator { get; }
            = new ConstraintValidator<Robot>();

        public Robot(int id, string name, string type)
        {
            Id = id < 0
                ? throw new ArgumentOutOfRangeException(nameof(id), $"Argument must be greater than or equal to zero")
                : id;

            Name = string.IsNullOrEmpty(name)
                ? throw new ArgumentNullException(nameof(name))
                : name;

            Type = string.IsNullOrEmpty(type)
                ? throw new ArgumentNullException(nameof(type))
                : type;
        }
        
        public void PutOnOrbit() =>
            JourneyStatus = JourneyStatus.OnOrbit;
        
        public void PutOnLand(Position position, Direction direction)
        {
            if (JourneyStatus == JourneyStatus.OnTravel)
                throw new ConstraintException("Robot cannot land while traveling");

            if (JourneyStatus == JourneyStatus.OnLand)
                throw new ConstraintException("Robot is already on land");

            JourneyStatus = JourneyStatus.OnLand;
            Position = position;
            Direction = direction;
        }

        public void MoveForward()
        {
            if(JourneyStatus != JourneyStatus.OnLand)
                throw new ConstraintException("Robot must be on land to move forward");

            switch (Direction)
            {
                case Direction.North:
                    Position = Position.ShiftY(1);
                    break;
                case Direction.East:
                    Position = Position.ShiftX(1);
                    break;
                case Direction.South:
                    Position = Position.ShiftY(-1);
                    break;
                case Direction.West:
                    Position = Position.ShiftX(-1);
                    break;
            }
        }
        public void TurnRight()
        {
            if (JourneyStatus != JourneyStatus.OnLand)
                throw new ConstraintException("Robot must be on land to turn right");

            Direction = Direction.Right();
        }
        public void TurnLeft()
        {
            if (JourneyStatus != JourneyStatus.OnLand)
                throw new ConstraintException("Robot must be on land to turn left");

            Direction = Direction.Left();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var that = (Robot)obj;
            return that.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode() => Id;
    }
}
