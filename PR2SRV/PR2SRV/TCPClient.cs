using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PR2SRV
{
    public class TCPClient
    {
        public static string policyfile =
                "PD94bWwgdmVyc2lvbj0iMS4wIj8+CgkJCTwhRE9DVFlQRSBjcm9zcy1kb21haW4tcG9saWN5IFNZU1RFTSAiL3htbC9kdGRzL2Nyb3NzLWRvbWFpbi1wb2xpY3kuZHRkIj4KCQkJCgkJCTwhLS0gUG9saWN5IGZpbGUgZm9yIHhtbHNvY2tldDovL3NvY2tzLmV4YW1wbGUuY29tIC0tPgoJCQk8Y3Jvc3MtZG9tYWluLXBvbGljeT4gCgkJCQoJCQkgICA8IS0tIFRoaXMgaXMgYSBtYXN0ZXIgc29ja2V0IHBvbGljeSBmaWxlIC0tPgoJCQkgICA8IS0tIE5vIG90aGVyIHNvY2tldCBwb2xpY2llcyBvbiB0aGUgaG9zdCB3aWxsIGJlIHBlcm1pdHRlZCAtLT4KCQkJICAgPHNpdGUtY29udHJvbCBwZXJtaXR0ZWQtY3Jvc3MtZG9tYWluLXBvbGljaWVzPSJtYXN0ZXItb25seSIvPgoJCQkKCQkJICAgPGFsbG93LWFjY2Vzcy1mcm9tIGRvbWFpbj0iKiIgdG8tcG9ydHM9IjkwMDAtMTAwMDAiIC8+CgkJCQoJCQk8L2Nyb3NzLWRvbWFpbi1wb2xpY3k+AA=="
            ;

        public static string Username = "Lich";
        public static string setrank = "setRank`61";
        public static string setrank0 = "setRank`44";

        public static string customizeinfo0 =
                "setCustomizeInfo`16763904`0`16763904`0`2`16`2`38`1,2,7,3,4,8,10,11,13,12,9`1,2,3,4,5,6,7,8,9,10,16,14,17,12,15,13,11,18,19,20,21,23,24,26,30,32,34,22,38,39,35`1,2,3,4,5,6,7,8,9,13,11,10,12,14,15,16,17,20,19,18,22,25,24,31,28,34,38,39,35`1,2,3,4,5,6,7,8,9,12,11,10,15,13,14,16,19,17,18,20,23,22,21,26,29,34,38,39,35`80`58`56`44`0`0`16763955`-1`14614528`14614528`2,10`11,23,31,1,2,4,26,35`10,20,22,30,1,2,29,3,16,32,35`10,27,1,2,7,35,15"
            ;

        public static string login1 = "setLoginID`1512";
        public static string login2 = "loginSuccessful`1";
        public static string ping = "ping`1419600276";

        public static string unlockparts =
                "1,2,3,4,5,6,7,8,9,10,11,12,13,14`1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39`1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39`1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39"
            ;

        public static string hatc = "0";
        public static string headc = "0";
        public static string bodyc = "0";
        public static string feetc = "0";

        public static string hatid = "2";
        public static string headid = "37";
        public static string bodyid = "37";
        public static string feetid = "37";

        public static string hatc2 = "0";
        public static string headc2 = "0";
        public static string bodyc2 = "0";
        public static string feetc2 = "0";

        public static string speed = "75";
        public static string acc = "90";
        public static string jump = "46";
        public static string rank = "61";

        public static int counter;

        public static NetworkStream ns;
        public static StreamReader nsr;
        public static StreamWriter nsw;
        public static Socket socket0;
        public static Socket mclient;
        public static Stopwatch RaceStopwatch;

        public static string slotid;
        public static string slotnum;

        public static void Main(string[] args)
        {
            Console.WriteLine(
                "How do you want to use the server?\nPress ENTER to use PR2tools Client! (this is probably what you want)\nPress '1' if you want to use original PR2 client to access LevelEditor (advanced).");
            var originalclient = Console.ReadKey().KeyChar - '0';
            Console.WriteLine();
            // SERVER
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var ip = new IPEndPoint(ipAddress, 9160);

            socket0 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket0.Bind(ip);
            socket0.Listen(10);
            Console.WriteLine("Platform Racing 2 local server is listening!");
            Console.WriteLine("Visit pr2tools.github.io for instructions. Have Fun!");
            Connect();

            string servermsg = null;
            var smcon = false;
            RaceStopwatch = new Stopwatch();

            while (true)
            {
                var sm = -1;
                //if (ns.DataAvailable || smcon) {
                sm = nsr.Read();
                smcon = true;
                //}

                if (sm != -1)
                    servermsg += (char) sm;


                if (sm == 4 || sm == 0)
                {
                    Console.WriteLine("> " + servermsg);
                    switch (Action(servermsg))
                    {
                        case 0:
                            Console.WriteLine("< policyfile");
                            var mpolicyfile = Base64Decode(policyfile);
                            mclient.Send(Encoding.ASCII.GetBytes(mpolicyfile));
                            Reconnect();
                            break;
                        case 1:
                            if (originalclient == 1)
                            {
                                SimpleMsg("eef", login1);
                                SimpleMsg("1d0", login2);
                            }
                            else
                            {
                                SendMsg(login1); //eef 0
                                SendMsg(login2); //1d0 1
                            }
                            break;
                        case 2:
                            if (originalclient == 1)
                            {
                                SimpleMsg("10d", setrank0);
                                SimpleMsg("471", customizeinfo0);
                            }
                            else
                            {
                                SendMsg(setrank); //10d 2
                                SendMsg(CustomizeInfo()); //471 3
                            }
                            break;
                        case 3:
                            Console.WriteLine(":" + "CLOSED - Waiting for a new connection" + ":");
                            Reconnect();
                            break;
                        case 4:
                            if (originalclient == 1)
                            {
                            }
                            else
                            {
                                SendMsg(ping);
                            }
                            break;
                        case 5:
                            var vars = servermsg.Split('`');
                            slotid = vars[3];
                            slotnum = vars[4];
                            SendMsg("fillSlot" + slotid + "`" + slotnum[0] + "`" + Username + "`" + rank + "`me");
                            break;
                        case 6:
                            SendMsg("confirmSlot" + slotid + "`" + slotnum[0]);
                            SendMsg("forceTime`0");
                            var lvl = slotid.Split('_');
                            SendMsg("startGame`" + lvl[0]);
                            SendMsg(CreateLocalCharacter());
                            break;
                        case 7:
                            SendMsg("clearSlot" + slotid + "`" + slotnum[0]);
                            break;
                        case 8:
                            SendMsg("finishDrawing`" + slotnum[0]);
                            SendMsg("setHats0`" + hatid + "`" + hatc + "`" + hatc2);
                            SendMsg("beginRace`" + slotnum[0]);

                            RaceStopwatch.Reset();
                            RaceStopwatch.Start();
                            break;
                        case 9:
                            SetCustomizeInfo(servermsg);
                            break;
                        case 10:
                            var vars2 = servermsg.Split('`');
                            SendMsg("addEffect`Hat`" + vars2[3] + "`" + vars2[4] + "`"
                                    + vars2[5][0] + "`" + hatid + "`" + hatc + "`" + hatc2 + "`0");
                            SendMsg("setHats0`");
                            break;
                        case 11:
                            SendMsg("removeHat0`");
                            SendMsg("setHats0`" + hatid + "`" + hatc + "`" + hatc2);
                            break;
                        case 12:
                            SendMsg("finishTimes`" + Username + "`forfeit`1");
                            SendMsg("var0`beginRemove`1");
                            break;
                        case 13:
                            RaceStopwatch.Stop();
                            SendMsg("finishTimes`" + Username + "`" + RaceStopwatch.Elapsed.TotalSeconds + "`1");
                            SendMsg("var0`beginRemove`1");
                            break;
                        default:
                            break;
                    }

                    servermsg = null;
                    smcon = false;
                    nsw.Flush();
                }
            }
        }

        public static int Action(string data)
        {
            string[] actions =
            {
                "<policy-file-request/>\0", //0
                "request_login_id`", //1
                "get_customize_info`", //2
                "close`", //3
                "ping`", // 4
                "fill_slot`", // 5
                "confirm_slot", // 6
                "clear_slot", //7
                "finish_drawing", //8
                "set_customize_info", //9
                "loose_hat", //10
                "get_hat", //11
                "quit_race`", //12
                "finish_race" //13
            };

            for (var i = 0; i < actions.Length; i++)
                if (data.IndexOf(actions[i]) > -1)
                    return i;
            return -1;
        }

        public static string CustomizeInfo()
        {
            string[] mcustomizeinfo =
            {
                "setCustomizeInfo",
                hatc,
                headc,
                bodyc,
                feetc,
                hatid,
                headid,
                bodyid,
                feetid,
                unlockparts,
                speed,
                acc,
                jump,
                rank,
                "0", //ranktokens
                "0",
                hatc2,
                headc2,
                bodyc2,
                feetc2,
                unlockparts
            };
            return Separated(mcustomizeinfo);
        }

        public static string Separated(string[] stringvalues)
        {
            var returnstring = stringvalues[0];
            for (var i = 1; i < stringvalues.Length; i++)
                returnstring += "`" + stringvalues[i];
            return returnstring;
        }

        public static void SetCustomizeInfo(string customizer)
        {
            var vars = customizer.Split('`');
            hatc = vars[3];
            headc = vars[4];
            bodyc = vars[5];
            feetc = vars[6];
            hatc2 = vars[7];
            headc2 = vars[8];
            bodyc2 = vars[9];
            feetc2 = vars[10];
            hatid = vars[11];
            headid = vars[12];
            bodyid = vars[13];
            feetid = vars[14];
            speed = vars[15];
            acc = vars[16];
            jump = vars[17].Remove(vars[17].Length - 1);
            ;
        }

        public static string CreateLocalCharacter()
        {
            string[] localcharacter =
            {
                "createLocalCharacter",
                "0",
                speed,
                acc,
                jump,
                hatc,
                headc,
                bodyc,
                feetc,
                hatid,
                headid,
                bodyid,
                feetid,
                hatc2,
                headc2,
                bodyc2,
                feetc2
            };
            return Separated(localcharacter);
        }

        public static void Connect()
        {
            mclient = socket0.Accept();
            var newclient = (IPEndPoint) mclient.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}", newclient.Address, newclient.Port);

            ns = new NetworkStream(mclient);
            nsr = new StreamReader(ns);
            nsw = new StreamWriter(ns);
            nsw.Flush();
        }

        public static void Reconnect()
        {
            counter = 0;
            nsw.Close();
            nsr.Close();
            ns.Close();
            mclient.Shutdown(SocketShutdown.Both);
            mclient.Close();
            Connect();
        }


        public static string SimpleMsg(string hash, string msg)
        {
            var smsg = counter + "`" + msg;
            smsg = hash + "`" + smsg;
            var returnvalue = smsg + (char) 4;
            nsw.Write(returnvalue);
            counter++;
            if (counter == 12)
                counter++;

            Console.WriteLine("< " + returnvalue);
            return returnvalue;
        }

        public static string SendMsg(string msg)
        {
            var smsg = counter + "`" + msg;
            smsg = "xxx" + "`" + smsg;
            var returnvalue = smsg + (char) 4;
            nsw.Write(returnvalue);
            counter++;
            if (counter == 12)
                counter++;

            Console.WriteLine("< " + returnvalue);
            return returnvalue;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}