/*
Copyright (c) 2013, 2014 Paolo Patierno

All rights reserved. This program and the accompanying materials
are made available under the terms of the Eclipse Public License v1.0
and Eclipse Distribution License v1.0 which accompany this distribution. 

The Eclipse Public License is available at 
   http://www.eclipse.org/legal/epl-v10.html
and the Eclipse Distribution License is available at 
   http://www.eclipse.org/org/documents/edl-v10.php.

Contributors:
   Paolo Patierno - initial API and implementation and/or initial documentation
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uPLibrary.Networking.M2Mqtt.Managers
{
    /// <summary>
    /// Delegate for executing user authentication
    /// </summary>
    /// <param name="clientId">clientId of client curently attempting connection</param>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    /// <returns></returns>
    public delegate bool MqttUserAuthenticationDelegate(string clientId, string username, string password);


    /// <summary>
    /// Delegate for executing topic communication authentication
    /// </summary>
    /// <param name="clientId">Client ID</param>
    /// <param name="topic">Topic</param>
    /// <returns>True if authenticated, false otherwise.</returns>
    public delegate bool MqttPubSubAuthenticationDelegate(string clientId, string topic);

    /// <summary>
    /// Manager for User Access Control
    /// </summary>
    public class MqttUacManager
    {
        // user authentication delegate
        private MqttUserAuthenticationDelegate userAuth;

        /// <summary>
        /// User authentication method
        /// </summary>
        public MqttUserAuthenticationDelegate UserAuth
        {
            get { return this.userAuth; }
            set { this.userAuth = value; }
        }

        // pubSub authentication delegate
        private MqttPubSubAuthenticationDelegate pubSubAuth;

        /// <summary>
        /// PubSub authentication method
        /// </summary>
        public MqttPubSubAuthenticationDelegate PubSubAuth
        {
            get { return this.pubSubAuth; }
            set { this.pubSubAuth = value; }
        }

        /// <summary>
        /// Execute user authentication
        /// </summary>
        /// <param name="clientId">clientId of client curently attempting connection</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Access granted or not</returns>
        public bool UserAuthentication(string clientId, string username, string password)
        {
            if (this.userAuth == null)
                return true;
            else
                return this.userAuth(clientId, username, password);
        }

        /// <summary>
        /// Execute topic authentication
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="topic">Topic</param>
        /// <returns></returns>
        public bool PubSubAuthentication(string clientId, string topic)
        {
            if (this.pubSubAuth == null)
                return true;
            else
                return this.pubSubAuth(clientId, topic);
        }
    }
}
