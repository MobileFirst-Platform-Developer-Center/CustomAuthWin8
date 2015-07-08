/*
 * COPYRIGHT LICENSE: This information contains sample code provided in source code form. You may copy, modify, and distribute
 * these sample programs in any form without payment to IBM® for the purposes of developing, using, marketing or distributing
 * application programs conforming to the application programming interface for the operating platform for which the sample code is written.
 * Notwithstanding anything to the contrary, IBM PROVIDES THE SAMPLE SOURCE CODE ON AN "AS IS" BASIS AND IBM DISCLAIMS ALL WARRANTIES,
 * EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, ANY IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, SATISFACTORY QUALITY,
 * FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND ANY WARRANTY OR CONDITION OF NON-INFRINGEMENT. IBM SHALL NOT BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR OPERATION OF THE SAMPLE SOURCE CODE.
 * IBM HAS NO OBLIGATION TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS OR MODIFICATIONS TO THE SAMPLE SOURCE CODE.
 */
 
﻿using System;
using System.Collections.Generic;
using System.Text;
using IBM.Worklight;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;
using Microsoft.VisualBasic;
using Windows.UI;

namespace CustomAuthWin8
{
    class CustomChallengeHandler : ChallengeHandler
    {
        String username;
        String password;
        MainPage page;

        public CustomChallengeHandler(MainPage mainPage)
            : base("AuthRealm")
        {
            page = mainPage;
        }

        public override void handleChallenge(JObject response)
        {
            Debug.WriteLine("In CustomChallengeHandler handleChallenge");

            MainPage._this.showChallenge();
        }

        public void sendResponse(String username, String password)
        {
            Debug.WriteLine("In CustomChallengeHandler sendResponse");
            Dictionary<String, String> parms = new Dictionary<String, String>();

            parms.Add("username", username);
            parms.Add("password", password);

            submitLoginForm("my_custom_auth_request_url", parms, null, 0, "post");

        }

        public override bool isCustomResponse(WLResponse response)
        {
            Debug.WriteLine("In CustomChallengeHandler isCustomResponse");

            if (!(response.getResponseJSON()["authStatus"] == null) && response.getResponseJSON()["authStatus"].ToString().CompareTo("required") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void onSuccess(WLResponse resp)
        {
            Debug.WriteLine("Challenge handler success");

            MainPage._this.hideChallenge();

            submitSuccess(resp);
        }

        public override void onFailure(WLFailResponse failResp)
        {
            Debug.WriteLine("Challenge handler failure ");
        }
    }
}
