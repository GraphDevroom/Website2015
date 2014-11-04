
    // JenaEnergy
    function ServerSentEvents() {

        var LogEventsSource = null;

        if (window.EventSource !== undefined) {
            LogEventsSource = new EventSource('/streams/JenaEnergy');
            document.getElementById('EventSourceType').firstChild.innerHTML = 'Browser EventSource';
        } else {
            LogEventsSource = new EventSource2('/streams/JenaEnergy');
            document.getElementById('EventSourceType').firstChild.innerHTML = 'JavaScript EventSource';
        }


        LogEventsSource.onmessage = function (event) {

            LastEventId = event.lastEventId;

            var div = document.createElement('div');
            div.innerHTML = "Message: " + event.data + "<br>";

            document.getElementById('LogEventsDiv').appendChild(div);

        };


        LogEventsSource.addEventListener('error', function (event) {

            if (event.readyState == EventSource.CLOSED) {
                // Connection was closed.
            }

            if (event.data !== undefined) {

                var div = document.createElement('div');
                div.innerHTML = "Error: " + event.data + "<br>";

                document.getElementById('OnErrorDiv').appendChild(div);

            }

        }, false);


        LogEventsSource.addEventListener('OnError', function (event) {

            try {

                var data         = JSON.parse(event.data);
                var LastEventId  = event.lastEventId;
                var Timestamp    = new Date(data.Timestamp);
                var Message      = data.Message;

                var div = document.createElement('div');
                div.innerHTML = "<span class=\"Timestamp\">" + Timestamp.format('dd.mm.yyyy HH:MM:ss') + "</span>&nbsp;&nbsp;" +
                                "<span class=\"ErrorMessage\">" + Message + "</span><br />";

                document.getElementById('OnErrorDiv').appendChild(div);

            }
            catch (ex)
            {

                var ediv = document.createElement('div');
                ediv.innerHTML = "<span class=\"Timestamp\">" + new Date().format('dd.mm.yyyy HH:MM:ss') + "</span>&nbsp;&nbsp;" +
                                 "<span class=\"ErrorMessage\">An exception occured during 'OnError' message handling: " + event.data + "</span><br />";

                document.getElementById('OnErrorDiv').appendChild(ediv);

            }

        }, false);


        LogEventsSource.addEventListener('OnNewSensordata', function (event) {

            try {

                var data = JSON.parse(event.data);

                var Timestamp = new Date(data.Timestamp);
                var LastEventId = event.lastEventId;

                var StreamId = data.StreamId;

                var JDL_U1 = data.U1.toFixed(2);
                var JDL_U2 = data.U2.toFixed(2);
                var JDL_U3 = data.U3.toFixed(2);

                var JDL_I1 = data.I1.toFixed(2);
                var JDL_I2 = data.I2.toFixed(2);
                var JDL_I3 = data.I3.toFixed(2);

                var JDL_P1 = data.P1.toFixed(2);
                var JDL_P2 = data.P2.toFixed(2);
                var JDL_P3 = data.P3.toFixed(2);

                var JDL_Q1 = data.Q1.toFixed(2);
                var JDL_Q2 = data.Q2.toFixed(2);
                var JDL_Q3 = data.Q3.toFixed(2);

                var JDL_frequency = data.frequency.toFixed(2);

                var JDL_ErrorText = data.ErrorText;

                var JDL_QueryTime         = data.QueryTime;
                var JDL_QueryRetries      = data.QueryRetries;
                var JDL_ConnectionRetries = data.ConnectionRetries;

                document.getElementById(StreamId + '_Timestamp').innerHTML = Timestamp.format('dd.mm.yyyy HH:MM:ss');

                document.getElementById(StreamId + '_U1').innerHTML = JDL_U1;
                document.getElementById(StreamId + '_U2').innerHTML = JDL_U2;
                document.getElementById(StreamId + '_U3').innerHTML = JDL_U3;

                document.getElementById(StreamId + '_I1').innerHTML = JDL_I1;
                document.getElementById(StreamId + '_I2').innerHTML = JDL_I2;
                document.getElementById(StreamId + '_I3').innerHTML = JDL_I3;

                document.getElementById(StreamId + '_P1').innerHTML = JDL_P1;
                document.getElementById(StreamId + '_P2').innerHTML = JDL_P2;
                document.getElementById(StreamId + '_P3').innerHTML = JDL_P3;

                document.getElementById(StreamId + '_Q1').innerHTML = JDL_Q1;
                document.getElementById(StreamId + '_Q2').innerHTML = JDL_Q2;
                document.getElementById(StreamId + '_Q3').innerHTML = JDL_Q3;

                document.getElementById(StreamId + '_frequency').innerHTML = JDL_frequency;

                document.getElementById(StreamId + '_ErrorText').innerHTML = JDL_ErrorText;

                document.getElementById(StreamId + '_QueryTime').innerHTML = JDL_QueryTime;
                document.getElementById(StreamId + '_QueryRetries').innerHTML = JDL_QueryRetries;
                document.getElementById(StreamId + '_ConnectionRetries').innerHTML = JDL_ConnectionRetries;

                var div = document.createElement('div');

                div.innerHTML = StreamId + "/" +
                                Timestamp.format('dd.mm.yyyy HH:MM:ss') + " / " +
                                JDL_U1 + " / " +
                                JDL_U2 + " / " +
                                JDL_U3 + " / " +
                                JDL_I1 + " / " +
                                JDL_I2 + " / " +
                                JDL_I3 + " / " +
                                JDL_P1 + " / " +
                                JDL_P2 + " / " +
                                JDL_P3 + " / " +
                                JDL_Q1 + " / " +
                                JDL_Q2 + " / " +
                                JDL_Q3 + " / " +
                                JDL_frequency + " / " +
                                JDL_ErrorText + " / " +
                                JDL_QueryTime + " / " +
                                JDL_QueryRetries + " / " +
                                JDL_ConnectionRetries;

                document.getElementById('RAWData').insertBefore(div, document.getElementById('RAWData').firstChild);


                U1.push([c, JDL_U1]);
                U2.push([c, JDL_U2]);
                U3.push([c, JDL_U3]);

                I1.push([c, JDL_I1]);
                I2.push([c, JDL_I2]);
                I3.push([c, JDL_I3]);

                P1.push([c, JDL_P1]);
                P2.push([c, JDL_P2]);
                P3.push([c, JDL_P3]);

                Q1.push([c, JDL_Q1]);
                Q2.push([c, JDL_Q2]);
                Q3.push([c, JDL_Q3]);

                frequency.push([c, JDL_frequency]);

                c = c + 1;

                $.plot("#VoltageChart",       [{ label: "U1",  data: U1 },
                                               { label: "U2",  data: U2 },
                                               { label: "U3",  data: U3 }]);

                $.plot("#CurrentChart",       [{ label: "I1",  data: I1 },
                                               { label: "I2",  data: I2 },
                                               { label: "I3",  data: I3 }]);

                $.plot("#RealPowerChart",     [{ label: "P1",  data: P1 },
                                               { label: "P2",  data: P2 },
                                               { label: "P3",  data: P3}]);

                $.plot("#ReactivePowerChart", [{ label: "Q1",  data: Q1 },
                                               { label: "Q2",  data: Q2 },
                                               { label: "Q3",  data: Q3}]);

                $.plot("#FrequencyChart", [{ label: "frequency", data: frequency }]);


            }
            catch (ex) {
            }

        }, false);

    }
