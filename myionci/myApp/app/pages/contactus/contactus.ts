import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/contactus/contactus.html',
})
export class ContactUsPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}