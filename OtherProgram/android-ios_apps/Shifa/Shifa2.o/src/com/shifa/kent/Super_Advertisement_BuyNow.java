package com.shifa.kent;

import android.app.Activity;
import android.content.Context;
import android.os.Handler;
import android.widget.LinearLayout;

public class Super_Advertisement_BuyNow extends Activity {
    public boolean showBigAds = false;
    boolean chatactive = true;
    String notification_id = "";
    int notifyid = 1;
    Super_Library_AppClass SLAc;
    Handler mHandler;
    String SessionID = "";
    Context ctx;
    Activity parentActivity = new Activity();
    LinearLayout baseIdNotification;

    public Super_Advertisement_BuyNow(Context ctx, Activity c, LinearLayout baseIdNotification) {
        SLAc = new Super_Library_AppClass(ctx);
        SessionID = SLAc.RestoreSessionIndexID(".XXXXXXX");
        this.ctx = ctx;
        parentActivity = c;
        this.baseIdNotification = baseIdNotification;
    }


}
