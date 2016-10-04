import {App, IonicApp, Platform} from 'ionic-angular';
import {HomePage} from './pages/home/home';
import {JSONP_PROVIDERS} from 'angular2/http';
import {TabsPage} from './pages/tabs/tabs';
// Import Authentication handler
import {AUTH_PROVIDERS} from './pages/authentication/authproviders';
import {Authentication} from './pages/authentication/authentication';
// Import Firebase
import {FIREBASE_PROVIDERS, defaultFirebase, AngularFire, firebaseAuthConfig, AuthProviders, AuthMethods, FirebaseAuth} from 'angularfire2';

@App({
  templateUrl: 'build/app.html',
  config: {},
  providers: [
    AUTH_PROVIDERS,
    JSONP_PROVIDERS,
    FIREBASE_PROVIDERS,
    defaultFirebase('https://sweltering-inferno-6912.firebaseio.com/'),
    firebaseAuthConfig({
      provider: AuthProviders.Facebook,
      method: AuthMethods.Popup,
      remember: 'default',
      scope: ['facebook']
    }),
  ]
})
class MyApp {
  rootPage: any;
  constructor(private app: IonicApp, private platform: Platform, public auth: Authentication) {
    // Handle the app setup etc.
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      console.log('Platform ready');

      // Handle checking to see if the user is logged in and if so go to their main feed page
      // Otherwise go to the home screen
      if (this.auth.authenticated) {
        this.rootPage = TabsPage;
      }
      else {
        this.rootPage = HomePage;
      }

    });
  }
}
