/**
* Copyright 2015 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
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
