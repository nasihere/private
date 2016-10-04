import {NavController, Modal} from 'ionic-angular';
import {HomePage} from '../home/home';
import {Injectable} from 'angular2/core';
import {FirebaseAuth, AuthProviders, FirebaseAuthState, AuthMethods} from 'angularfire2';

@Injectable()
export class Authentication {
  private authState: FirebaseAuthData | FirebaseAuthState;

  constructor(public auth$: FirebaseAuth) {
    auth$.subscribe((state: FirebaseAuthState) => {
        this.authState = state;
    });
  }

  // Check to see if the user is authenticated
  get authenticated(): boolean {
    return this.authState !== null && !this.expired;
  }

  // Checkt to see if the auth has expired
  get expired(): boolean {
    // FirebaseAuthState is currently missing `expires` field
    // @see https://github.com/angular/angularfire2/issues/112
    return !this.authState || (this.authState.expires * 1000) < Date.now();
  }

  // Get the unique user id
  get id(): string {
    return this.authenticated ? this.authState.uid : '';
  }

  // Login via Facebook
  // Call back is: https://auth.firebase.com/v2/sweltering-inferno-6912/auth/facebook/callback
  signInWithFacebook(): Promise<FirebaseAuthState> {
    return this.auth$.login({
      provider: AuthProviders.Facebook,
      method: AuthMethods.Popup,
      scope: ['public_profile,user_friends,email']     // Add user_actions.fitness, user_birthday, user_likes, user_location
    });
  }

  // Login via Google
  signInWithGoogle(): Promise<FirebaseAuthState> {
    return this.auth$.login({
      provider: AuthProviders.Google,
      method: AuthMethods.Popup
    });
  }

  // Login via Twitter
  signInWithTwitter(): Promise<FirebaseAuthState> {
    return this.auth$.login({
      provider: AuthProviders.Twitter,
      method: AuthMethods.Popup
    });
  }

  // Logout
  signOut(): void {
    this.auth$.logout();
  }

}
