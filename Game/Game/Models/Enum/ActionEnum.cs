namespace Game.Models
{
    /// <summary>
    /// The Types of s a Action can have
    /// Used in Action Crudi, and in Battles.
    /// </summary>
    public enum ActionEnum
    {
        // Not specified
        Unknown = 0,

        // Attack
        Attack = 1,

        // Move
        Move = 10,

        // Ability
        Ability = 20,

        //Focused attack
        FocusedAttack = 25
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class ActionEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this ActionEnum value)
        {
            // Default String
            var Message = "None";

            switch (value)
            {
                case ActionEnum.Attack:
                    Message = " Attacks ";
                    break;

                case ActionEnum.Move:
                    Message = " Moves ";
                    break;

                case ActionEnum.Ability:
                    Message = " Uses Ability ";
                    break;

                case ActionEnum.FocusedAttack:
                    Message = " Uses Focused Attack ";
                    break;

                case ActionEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }

        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToImageURI(this ActionEnum value)
        {
            // Default String
            var Message = "action_default.png";

            switch (value)
            {
                case ActionEnum.Attack:
                    Message = "knife.png";
                    break;

                case ActionEnum.Move:
                    Message = "nonslip.png";
                    break;

                case ActionEnum.Ability:
                    Message = "apron.png";
                    break;

                case ActionEnum.FocusedAttack:
                    Message = "rollingpin.png";
                    break;

                case ActionEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }
}