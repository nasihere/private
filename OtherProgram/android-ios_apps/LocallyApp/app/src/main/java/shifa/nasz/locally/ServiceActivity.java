package shifa.nasz.locally;

import android.app.Activity;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.util.Log;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

public class ServiceActivity extends Service {

    private static final String TAG = "MyService";
    List<NameValuePair> nameValuePairs;
    public boolean DownloadTaskBusy = false;

    public boolean ServiceStatus = false;
    private NotificationManager myNotificationManager;
    private int notificationIdOne = 111;
    private String SessionID = "";
    protected int notificationSleep = 10000; // default should put 10 sec otherwise service will set how frequently take it
    protected String notificationResponse = "";
    protected String notificationContentTitle = "Locally";
    protected String notificationContentText = "New message from javacodegeeks received";
    protected String notificationContentTicker = "Locally App";
    protected String notificationContentId = "100";
    protected String notificationContentScreen = "MainPage";
    int numMessagesOne = 10;
    public Context ctx;
    public GPSTracker GPS;//
    public   double geolat = 0;
    public  double geolng = 0;
    Super_Library_AppClass SLAc;// = new Super_Library_AppClass(ctx);

    @Override
    public IBinder onBind(Intent arg0) {
        return null;
    }

    @Override
    public void onCreate() {
        ctx = this;
        SLAc = new Super_Library_AppClass(ctx);
       // Toast.makeText(this, "Congrats! MyService Created", Toast.LENGTH_LONG).show();
         //     Log.d(TAG, "onCreate");
     //   displayNotificationOne();

    }

    protected void displayNotificationOne() {

        Super_Library_AppClass SLAc  = new Super_Library_AppClass(ctx);
        SLAc.SavePreference("notificationResponse",notificationResponse);
        // Invoking the default notification service
        android.support.v4.app.NotificationCompat.Builder  mBuilder = new android.support.v4.app.NotificationCompat.Builder(ctx);

        mBuilder.setContentTitle(notificationContentTitle);
        mBuilder.setContentText(notificationContentText);
        mBuilder.setTicker(notificationContentTicker);
        mBuilder.setSmallIcon(R.drawable.ic_launcher);

        // Increase notification number every time a new notification arrives
        mBuilder.setNumber(++numMessagesOne);

        // Creates an explicit intent for an Activity in your app
        Intent resultIntent = new Intent(ctx, activity_kent.class);
        resultIntent.putExtra("notificationId", notificationContentId);
        resultIntent.putExtra("notificationContentScreen", notificationContentScreen);

        //This ensures that navigating backward from the Activity leads out of the app to Home page
        android.support.v4.app.TaskStackBuilder stackBuilder = android.support.v4.app.TaskStackBuilder.create(this);

        // Adds the back stack for the Intent
        stackBuilder.addParentStack(activity_kent.class);

        // Adds the Intent that starts the Activity to the top of the stack
        stackBuilder.addNextIntent(resultIntent);
        PendingIntent resultPendingIntent =
                stackBuilder.getPendingIntent(
                        0,
                        PendingIntent.FLAG_ONE_SHOT //can only be used once
                );
        // start the activity when the user clicks the notification text
        mBuilder.setContentIntent(resultPendingIntent);

        myNotificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        // pass the Notification object to the system
        myNotificationManager.notify(notificationIdOne, mBuilder.build());
    }

    public Runnable NameOfRunnable = new Runnable()
    {
        @Override
        public void run()
        {
            /*while (ServiceStatus)
            {
             // TODO add code to refresh in background
                try
                {
                    Log.d(TAG, "Checking update " + DateFormat.getDateTimeInstance().format(new Date()).toString());
                    if (DownloadTaskBusy == false)
                    {

                        nameValuePairs =  new ArrayList<NameValuePair>(2);
                        nameValuePairs.add(new BasicNameValuePair("mobile", SessionID));
                        DownloadWebPageTask task = new DownloadWebPageTask();
                        task.execute(new String[]{"http://kent.nasz.us/mumbra/php/notification.php"});
                    }
                    Thread.sleep(notificationSleep);// sleeps 1 second




                }
                catch (InterruptedException e)
                {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
            }*/

        }
    };
    private void
    GetUpdateServer(){
        ServiceStatus = true;

        Super_Library_AppClass SLAc = new Super_Library_AppClass(ctx);
        SessionID = SLAc.GetPreferenceValue("ProfileMobile");
        //Thread name = new Thread(NameOfRunnable);
        //name.start();


        Handler handler = new Handler()
        {
            @Override
            public void handleMessage(Message msg)
            {

                switch (msg.what)
                {
                    case HttpPostThread.SUCCESS:
                        String answer = (String)msg.obj;
                        if (answer != null)
                        {
                            try {
                                Log.e(TAG,"message handleMessage: "+answer);
                                NotficationDataCallBack(answer);
                            } catch (Exception e) {
                                e.printStackTrace();
                                Log.e(TAG,"Error handleMessage: ",e);
                            }
                        }
                        break;

                    case HttpPostThread.FAILURE:
                        // do some error handeling
                        break;

                    default:
                        break;
                }
            }
        };
        ArrayList<NameValuePair> pairs = new ArrayList<NameValuePair>();
        pairs.add(new BasicNameValuePair("key", "value"));
        HttpPostThread thread = new  HttpPostThread("http://kent.nasz.us/mumbra/php/notification.php",pairs, handler);
        thread.start();

    }
    protected void NotficationDataCallBack(String result) {

        try {
            if (!notificationResponse.equalsIgnoreCase(result)) {
                Log.e(TAG,"Thread response execution started ");
                notificationResponse = result;
                String[] resultSplit = notificationResponse.split("#_#");
                notificationSleep = Integer.parseInt(resultSplit[0]) * 1000;
                notificationContentTitle = resultSplit[1];
                notificationContentText = resultSplit[2];
                notificationContentTicker = resultSplit[3];
                notificationContentScreen = resultSplit[4];
                notificationContentId = resultSplit[5];
                String OldnotificationResponse = SLAc.GetPreferenceValue("notificationResponse");
                if (OldnotificationResponse.equalsIgnoreCase(notificationResponse)){
                    Log.e(TAG,"OldnotificationResponse is same with new notification.. program will not display notification");
                    return;
                }

                displayNotificationOne();

                if (resultSplit[6].equalsIgnoreCase("GPS")){
                    try {
                        GPSTracker GPS = new GPSTracker(ctx);
                        GPS.GPSActivate();
                    }catch (Exception ex){
                        nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
                        nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
                        Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

                    }

                }
            }

        } catch (Exception ex) {
            Log.e(TAG, "PostExecute ", ex);
        }
        DownloadTaskBusy = false;
    }
    private Timer timer;

    private TimerTask updateTask = new TimerTask() {
        @Override
        public void run() {
            Log.i(TAG, "Timer task doing work");
            GetUpdateServer();
        }
    };

    @Override
    public void onStart(Intent intent, int startId) {

        //Toast.makeText(this, "My Service Started", Toast.LENGTH_LONG).show();
        Log.d(TAG, "onStart");
       // Timer timer = new Timer("TweetCollectorTimer");
       // timer.schedule(updateTask, 1000L, 60 * 1000L);
        GetUpdateServer();
        //Note: You can start a new thread and use it for long background processing from here.
    }

    @Override
    public void onDestroy() {
        ServiceStatus = false;
       // Toast.makeText(this, "MyService Stopped", Toast.LENGTH_LONG).show();
        Log.d(TAG, "onDestroy");
    }

/*

    private class DownloadWebPageTask extends AsyncTask<String, Context, String> {

        @Override
        protected String doInBackground(String... urls) {
            DownloadTaskBusy = true;
            Log.e(TAG, "enter doInBackground");
            String response = "";
            String uri = "";
            for (String url : urls) {
                uri = url;
                Log.e("uri", uri);
                try {
                    DefaultHttpClient client = new DefaultHttpClient();
                    HttpPost httpPost = new HttpPost(url);
                    try {
                        httpPost.setEntity(new UrlEncodedFormEntity(nameValuePairs));
                        HttpResponse execute = client.execute(httpPost);
                        InputStream content = execute.getEntity().getContent();
                        BufferedReader buffer = new BufferedReader(new InputStreamReader(content));
                        String s = "";
                        while ((s = buffer.readLine()) != null) {
                            response += s;
                        }

                    } catch (Exception e) {
                        Log.e(TAG, "Error http:", e);
                        e.printStackTrace();
                        return "-404-";
                    }
                    return response;
                } catch (Exception ex) {

                    Log.e(TAG, "Error http:", ex);
                    return "-405-";
                }

            }


            return "";
        }

        @Override
        protected void onPostExecute(String result) {

            try {
                Log.e("SLR", "Result " + result);
                if (!notificationResponse.equalsIgnoreCase(result)) {
                    notificationResponse = result;
                    String[] resultSplit = notificationResponse.split("#_#");
                    notificationSleep = Integer.parseInt(resultSplit[0]) * 1000;
                    notificationContentTitle = resultSplit[1];
                    notificationContentText = resultSplit[2];
                    notificationContentTicker = resultSplit[3];
                    notificationContentScreen = resultSplit[4];
                    notificationContentId = resultSplit[5];
                    String OldnotificationResponse = SLAc.GetPreferenceValue("notificationResponse");
                    if (OldnotificationResponse.equalsIgnoreCase(notificationResponse)){
                        Log.e(TAG,"OldnotificationResponse is same with new notification.. program will not display notification");
                        return;
                    }

                    displayNotificationOne();

                    if (resultSplit[6].equalsIgnoreCase("GPS")){
                        try {
                            GPSTracker GPS = new GPSTracker(ctx);
                            GPS.GPSActivate();
                        }catch (Exception ex){
                            nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
                            nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
                            Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) ctx));

                        }

                    }
                }

            } catch (Exception ex) {
                Log.e(TAG, "PostExecute ", ex);
            }
            DownloadTaskBusy = false;
        }
    }*/
}








 /* protected void displayNotificationTwo() {
       // Invoking the default notification service
        NotificationCompat.Builder  mBuilder = new NotificationCompat.Builder(ctx);

        mBuilder.setContentTitle("New Message with implicit intent");
        mBuilder.setContentText("New message from javacodegeeks received...");
        mBuilder.setTicker("Implicit: New Message Received!");
        mBuilder.setSmallIcon(R.drawable.ic_launcher);

        NotificationCompat.InboxStyle inboxStyle = new NotificationCompat.InboxStyle();

        String[] events = new String[3];
        events[0] = new String("1) Message for implicit intent");
        events[1] = new String("2) big view Notification");
        events[2] = new String("3) from javacodegeeks!");

        // Sets a title for the Inbox style big view
        inboxStyle.setBigContentTitle("More Details:");
        // Moves events into the big view
        for (int i=0; i < events.length; i++) {
            inboxStyle.addLine(events[i]);
        }
        mBuilder.setStyle(inboxStyle);

        // Increase notification number every time a new notification arrives
        mBuilder.setNumber(++numMessagesOne);

        // When the user presses the notification, it is auto-removed
        mBuilder.setAutoCancel(true);

        // Creates an implicit intent
        Intent resultIntent = new Intent(ctx, activity_kent.class);
        resultIntent.putExtra("notificationId", notificationContentId);
        resultIntent.putExtra("notificationContentScreen", notificationContentScreen);

        android.support.v4.app.TaskStackBuilder stackBuilder = android.support.v4.app.TaskStackBuilder.create(this);
        stackBuilder.addParentStack(activity_kent.class);

        stackBuilder.addNextIntent(resultIntent);
        PendingIntent resultPendingIntent =
                stackBuilder.getPendingIntent(
                        0,
                        PendingIntent.FLAG_ONE_SHOT
                );
        mBuilder.setContentIntent(resultPendingIntent);

        myNotificationManager = (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        myNotificationManager.notify(notificationIdOne, mBuilder.build());

    }*/