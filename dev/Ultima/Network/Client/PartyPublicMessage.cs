﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimaXNA.Core.Network.Packets;

namespace UltimaXNA.Ultima.Network.Client
{
    public class PParty_PublicMessage : SendPacket
    {
        public PParty_PublicMessage(string text) : base(0xbf, "Public Party Message")
        {
            Stream.Write((short)6);
            Stream.Write((byte)4);
            Stream.WriteBigUniNull(text);
            Stream.Write((short)0);
        }
    }
}
