package com.shifa.kent.chatsdk;


import android.os.Bundle;
import android.os.Handler;
import android.os.ResultReceiver;

public class chat_download_receiver extends ResultReceiver {
    private Receiver mReceiver;

    public chat_download_receiver(Handler handler) {
        super(handler);
    }

    public void setReceiver(Receiver receiver) {
        mReceiver = receiver;
    }

    @Override
    protected void onReceiveResult(int resultCode, Bundle resultData) {
        if (mReceiver != null) {
            mReceiver.onReceiveResult(resultCode, resultData);
        }
    }

    public interface Receiver {
        public void onReceiveResult(int resultCode, Bundle resultData);
    }
}