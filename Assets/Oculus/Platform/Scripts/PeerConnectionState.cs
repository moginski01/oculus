// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

using System.ComponentModel;

namespace Oculus.Platform
{
    using Description = DescriptionAttribute;

    public enum PeerConnectionState
    {
        [System.ComponentModel.Description("UNKNOWN")]
        Unknown,

        /// Connection to the peer is established.
        [System.ComponentModel.Description("CONNECTED")]
        Connected,

        /// A timeout expired while attempting to (re)establish a connection. This can
        /// happen if peer is unreachable or rejected the connection.
        [System.ComponentModel.Description("TIMEOUT")]
        Timeout,

        /// Connection to the peer is closed. A connection transitions into this state
        /// when it is explicitly closed by either the local or remote peer calling
        /// Net.Close(). It also enters this state if the remote peer no longer
        /// responds to our keep-alive probes.
        [System.ComponentModel.Description("CLOSED")]
        Closed
    }
}