﻿using System;

namespace FastMDX {
    class ParsingException : Exception {
        public override string Message => "Parsing error.";
    }

    class NodeNameCantBeEmptyException : Exception {
        public override string Message => "Node name can't be empty.";
    }
}
