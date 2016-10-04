import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Pages to use
import {RegisterVerifyPage} from '../registerverify/registerverify';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/register/register.html',
  providers: [MoreShared]
})
export class RegisterPage {
  // Definitions
  public registerForm: any;             // Registration form info.
  public moreshared: any;               // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared) {
    this.registerForm = fb.group({
      countrycode: ['', Validators.required],
      mobile: ['', Validators.required]
    });
    this.moreshared = moreshared;
  }

  // Handle fabric phone register
  handleDigitsregister() {
    this.nav.push(RegisterVerifyPage);
  }

  // Handle Facebook register
  handleFacebookRegister() {
    console.log('handleFacebookRegister');
  }

  // Handle twitter register
  handleTwitterRegister() {
    console.log('handleTwitterRegister');
  }

  // Handle going to the TOS view
  openTOS() {
    this.moreshared.openTOS();
  }

  // Handle going to the Privacy view
  openPrivacy() {
    this.moreshared.openPrivacy();
  }

  // Handle going to the Rewards Terms view
  openRewardsTerms() {
    this.moreshared.openRewardsTerms();
  }

  // Handle going to the About Us view
  openAboutUs() {
    this.moreshared.openAboutUs();
  }
}