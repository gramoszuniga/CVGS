using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CVGS.Enumerations
{
    public class GameEnums
    {
        public enum Publisher
        {
            [EnumMember(Value = "Epic Games Inc.")]
            EpicGames,
            Nintendo,
            RockstarGames,
            MicrosoftStudios
        }
    }
}      