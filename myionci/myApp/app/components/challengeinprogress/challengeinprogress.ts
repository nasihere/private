import {Component} from 'angular2/core';
import {IONIC_DIRECTIVES} from 'ionic-angular';

@Component({
  selector: 'challengeinprogress',
  templateUrl: 'build/components/challengeinprogress/challengeinprogress.html',
  inputs: ['feedinfo', 'userprofile'],
  directives: [IONIC_DIRECTIVES]
})
export class Challengeinprogress {
  constructor() {
  }

  // Handle the Challenge Completed action from the slider
  handleCompleted(feedinfo) {
    console.log('handleCompleted');
  }

  // Handle the New Challenge action from the slider
  handleNewChallenge(feedinfo) {
    console.log('handleNewChallenge');
  }

  // Handle the Challenge Smack Talk action from the slider
  handleSmackTalk(feedinfo) {
    console.log('handleSmackTalk');
  }

  // Handle the Socialize action from the slider
  handleSocialize(feedinfo) {
    console.log('handleSocialize');
  }
}
