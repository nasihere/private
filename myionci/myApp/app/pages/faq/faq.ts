import {Page, ViewController} from 'ionic-angular';

@Page({
  templateUrl: 'build/pages/faq/faq.html',
})
export class FAQPage {
  constructor(private viewCtrl: ViewController) {
  }

  // Handle closing the modal
  close() {
    this.viewCtrl.dismiss();
  }
}
