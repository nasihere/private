package shifa.nasz.locally;

/**
 * Created by Nasz on 1/19/15.
 */



import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.ListView;

public class activity_event extends Activity {
    ProgressDialog progressDialog ;
    ListView listView;
    public Super_Library_AppClass SLAc;
    String source = "";
    private WebView webView;
    public String SessionID = "";
    Context ctx;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.webview_events);
        ctx = this;

        SLAc = new Super_Library_AppClass(this);
        SessionID  = SLAc.RestoreSessionIndexID("session_id");

        String url = IntentBunddle("url");
        try
        {
            source= IntentBunddle("source");
        }
        catch(Exception ex){}
        webView = (WebView) findViewById(R.id.webView);

        webView.getSettings().setJavaScriptEnabled(true);

        Log.e("URL",url);

        //String customHtml = "<html><body><h2>Greetings from JavaCodeGeeks</h2></body></html>";
        //webView.loadData(customHtml, "text/html", "UTF-8");

        webView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                Log.e("URL", url);


                    view.loadUrl(url);
                return false;
            }
        });

        webView.loadUrl(url);
    }

    private String IntentBunddle(String Session_Key)
    {
        try
        {
            Bundle extras = getIntent().getExtras();
            String SessionID = "";
            if (extras != null) {
                SessionID = extras.getString(Session_Key);
                return SessionID ;
            }
        }
        catch(Exception ex)
        {
            return "http://shifa.nasz.us/events.html";
        }
        return "";
    }
    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(event.getAction() == KeyEvent.ACTION_DOWN){
            switch(keyCode)
            {
                case KeyEvent.KEYCODE_BACK:
                    if(webView.canGoBack() == true){
                        webView.goBack();
                    }else{
                        //Intent intent = new Intent(activity_events.this, home_menu.class);
                        //startActivity(intent);
                        finish();

                    }
                    return true;
            }

        }
        return super.onKeyDown(keyCode, event);



    }
}