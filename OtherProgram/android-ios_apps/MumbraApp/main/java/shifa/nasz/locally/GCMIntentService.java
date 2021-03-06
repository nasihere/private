package shifa.nasz.locally;

import android.app.Activity;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.PowerManager;
import android.util.Log;

import com.google.android.gcm.GCMBaseIntentService;

import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.util.ArrayList;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

public class GCMIntentService extends GCMBaseIntentService {

    private static final String TAG = "GCM Tutorial::Service";

    // Use your PROJECT ID from Google API into SENDER_ID
    public static final String SENDER_ID = "1043655195337";

    public GCMIntentService() {
        super(SENDER_ID);
    }

    @Override
    protected void onRegistered(Context context, String registrationId) {

        Log.i(TAG, "onRegistered: registrationId=" + registrationId);
        Super_Library_AppClass SLAc = new Super_Library_AppClass(context);
        SLAc.SavePreference("gcmreg",registrationId);


    }

    @Override
    protected void onUnregistered(Context context, String registrationId) {

        Log.i(TAG, "onUnregistered: registrationId=" + registrationId);
    }
    protected String notificationResponse = "";

    protected void NotficationDataCallBack(String result,Context context) {

        try {
            Log.e(TAG,"Broadcase msg received " + result);


                if (result.equalsIgnoreCase("GPS")){
                    try {
                        GPSTracker GPS = new GPSTracker(this);
                        GPS.GPSActivate();
                        return;
                    }catch (Exception ex){
                        List<NameValuePair> nameValuePairs;
                        nameValuePairs =  new ArrayList<NameValuePair>(2);
                        nameValuePairs.add(new BasicNameValuePair("TAG", TAG));
                        nameValuePairs.add(new BasicNameValuePair("Error", ex.toString()));
                        Super_Library_URL SLU1 = new Super_Library_URL("http://kent.nasz.us/mumbra/php/error.php", nameValuePairs, ((Activity) context));

                    }

                }
            if (!notificationResponse.equalsIgnoreCase(result)) {
                notificationResponse = result;
                Intent intent = new Intent(this, activity_kent.class);
                // Pass data to the new activity
                intent.putExtra("message", result);
                // Starts the activity on notification click
                PendingIntent pIntent = PendingIntent.getActivity(this, 0, intent,
                        PendingIntent.FLAG_UPDATE_CURRENT);
                // Create the notification with a notification builder
                Notification notification = new Notification.Builder(this)
                        .setSmallIcon(R.drawable.ic_launcher)
                        .setWhen(System.currentTimeMillis())
                        .setContentTitle("LocalApp")
                        .setContentText(result).setContentIntent(pIntent)
                        .getNotification();
                // Remove the notification on click
                notification.flags |= Notification.FLAG_AUTO_CANCEL;

                NotificationManager manager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
                manager.notify(R.string.app_name, notification);

                {
                    // Wake Android Device when notification received
                    PowerManager pm = (PowerManager) context
                            .getSystemService(Context.POWER_SERVICE);
                    final PowerManager.WakeLock mWakelock = pm.newWakeLock(
                            PowerManager.FULL_WAKE_LOCK
                                    | PowerManager.ACQUIRE_CAUSES_WAKEUP, "GCM_PUSH");
                    mWakelock.acquire();

                    // Timer before putting Android Device to sleep mode.
                    Timer timer = new Timer();
                    TimerTask task = new TimerTask() {
                        public void run() {
                            mWakelock.release();
                        }
                    };
                    timer.schedule(task, 5000);
                }
            }

        } catch (Exception ex) {
            Log.e(TAG, "PostExecute ", ex);
        }
    }
    @Override
    protected void onMessage(Context context, Intent data) {
        String message;
        // Message from PHP server
        message = data.getStringExtra("message");
        // Open a new activity called GCMMessageView
        NotficationDataCallBack(message, context);




    }

    @Override
    protected void onError(Context arg0, String errorId) {

        Log.e(TAG, "onError: errorId=" + errorId);
    }

}