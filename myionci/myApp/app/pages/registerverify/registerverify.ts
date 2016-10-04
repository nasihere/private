import {Page, NavController, NavParams} from 'ionic-angular';
import {Validators, FormBuilder} from 'angular2/common';
// Import Pages to use
import {RequiredPage} from '../required/required';
// Import Footer handler
import {MoreShared} from '../moreshared/moreshared';

@Page({
  templateUrl: 'build/pages/registerverify/registerverify.html',
  providers: [MoreShared]
})
export class RegisterVerifyPage {
  // Definitions
  public registerVerifyForm: any;         // Registration verify form info.
  public moreshared: any;                 // Shared handler for footer elements

  constructor(private nav: NavController, fb: FormBuilder, moreshared: MoreShared) {
    this.registerVerifyForm = fb.group({
      code: ['', Validators.required]
    });
    this.moreshared = moreshared;
  }

  // Handle fabric phone register verification
  handleDigitsVerify() {
    console.log('handleDigitsVerify');
    this.nav.push(RequiredPage);
  }

  // Handle a fabric resend of the code
  handleResendCode() {
    console.log('handleResendCode');
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