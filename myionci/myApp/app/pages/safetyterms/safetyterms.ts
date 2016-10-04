import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/safetyterms/safetyterms.html',
})
export class SafetyTermsPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
