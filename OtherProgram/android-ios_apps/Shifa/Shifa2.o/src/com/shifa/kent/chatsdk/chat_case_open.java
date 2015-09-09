package com.shifa.kent.chatsdk;

import android.app.ActionBar;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.shifa.kent.R;

import java.util.UUID;


public class chat_case_open extends Activity {
    final String POST_MSG_STRING = "#=#<font color=red>Basic Details</font>#-#" +
            "Problem: #-#" +
            "Age: #-# Gender: #-#" +
            "Weight: #-# Height: #-#" +
            "Marital Status:  #-#" +
            "Do you have any children? #-#" +
            "How many?#-#" +
            "Family medical history: #-#" +
            "Past history of health: #-#" +
            "Have you taken medications in the past? If so, which medications have you taken? #-#" +
            "Do you have any allergies? #-#" +
            "Chief complaint? #-#" +
            "#=#<font color=red><b><u>Modalities</u></b> #-#" +
            "How is your appetite? #-#" +
            "What or who do you dislike? #-#" +
            "What do you have a strong desire for? #-#" +
            "What do you thirst for? #-#" +
            "What do you have an addiction to? #-#" +
            "What do you fear most? #-#" +
            "How would you describe your sleep? #-#" +
            "Any Extra comment:  #-#";
    final String POST_MSG_STRING_1 = "#=#<font color=red>Advanced Details</font>#-#" +
            "Problem: #-#" +
            "Name: #-#Date of birth: #-#Gender: #-#Weight: #-# Height: #-# Marital Status: #-# Do you have any children?:#-#" +
            "How many?:#-# Family medical history: #-#" +
            "Past history of health: #-#" +
            "Have you been hospitalized? #-#" +
            "Date: #-#" +
            "Where?#-#" +
            "Have you taken medications in the past? If so, which medications have you taken?#-#" +
            "What was your response to the medication?#-#" +
            "Have you been vaccinated? If so, what vaccines have you received?#-#" +
            "Do you have any allergies?#-#" +
            "List the number of dental fillings:#-#" +
            "#=#<font color=red><u>General Symptoms</u></font>#-#" +
            "Are you presently taking medications? If so,  which medications are you taking?#-#" +
            "Chief complaint?#-#" +
            "When did it first occur?#-#" +
            "What is your symptom?#-#" +
            "Describe the sensation experienced.#-#" +
            "Give the precise location.#-#" +
            "List any concomitant symptoms.#-#" +
            "#=#<font color=red>Modalities</font>#-#" +
            "#=#<font color=red>When do your symptoms get worse?#-#" +
            "#@#morning: #-#" +
            "#@#evening: #-#" +
            "#@#night: #-#" +
            "#@#during sleep: #-#" +
            "#@#on rising: #-#" +
            "#@#before a thunderstorm: #-#" +
            "#@#after a thunderstorm: #-#" +
            "#@#in damp weather: #-#" +
            "#@#in cold weather: #-#" +
            "#@#wearing tight clothes: #-#" +
            "#@#in a warm room: #-#" +
            "#@#while washing oneself : #-#" +
            "#@#in bed: #-#" +
            "#@#while moving around: #-#" +
            "#@#during rest: #-#" +
            "#@#in a closed room: #-#" +
            "#@#in crowded places: #-#" +
            "#@#in the winter: #-#" +
            "#@#in the summer: #-#" +
            "#@#in the spring: #-#" +
            "#@#in the autumn: #-#" +
            "#@#at the sea: #-#" +
            "#@#in the sunlight: #-#" +
            "#@#lying on ones back: #-#" +
            "#@#lying on affected side: #-#" +
            "#@#while exercising: #-#" +
            "#@#while talking: #-#" +
            "#@#when the temperature changes: #-#" +
            "#@#at the smell of tobacco: #-#" +
            "#@#on missing a meal: #-#" +
            "#@#on the onset of a menstruation: #-#" +
            "#@#in cold, wet weather: #-#" +
            "#@#in bed:#-#" +
            "#=#<font color=red>When do your symptoms get better?#-#" +
            "#@#bend backwards: #-#" +
            "#@#doubled over: #-#" +
            "#@#upon moving: #-#" +
            "#@#resting: #-#" +
            "#@#with cold applications: #-#" +
            "#@#warm applications: #-#" +
            "#@#in the morning: #-#" +
            "#@#in the evening: #-#" +
            "#@#in the night: #-#" +
            "#@#listening to music: #-#" +
            "#@#applying pressure: #-#" +
            "#@#uncovering the feet in bed: #-#" +
            "#@#during the summer:#-#" +
            "#@#at the sea: #-#" +
            "#@#outdoors: #-#" +
            "#@#opening a window: #-#" +
            "#@#left alone: #-#" +
            "#@#while eating: #-#" +
            "#@#on the onset of a menstruation: #-#" +
            "#=#<font color=red>How is your appetite? #-#" +
            "#@#good: #-#" +
            "#@#decreased: #-#" +
            "#@#never hungry: #-#" +
            "#@#always hungry: #-#" +
            "#@#capricious eater:#-#" +
            "#@#cant finish a meal: #-#" +
            "#@#hungry soon after a meal: #-#" +
            "#=#<font color=blue>List any eating disorder #-#" +
            "#=#<font color=red>What do you have an aversion to?#-#" +
            "#@#food :#-#" +
            "#@#water:#-#" +
            "#@#meat :#-#" +
            "#@#bread:#-#" +
            "#@#fish:#-#" +
            "#@#shellfish: #-#" +
            "#@#butter: #-#" +
            "#@#eggs : #-#" +
            "#@#fruits: #-#" +
            "#@#milk:#-#" +
            "#@#onions: #-#" +
            "#@#pickles: #-#" +
            "#@#wine: #-#" +
            "#@#vegetables:#-#" +
            "#@#cabbage: #-#" +
            "#@#beans: #-#" +
            "#@#pork: #-#" +
            "#@#potato : #-#" +
            "#@#soup: #-#" +
            "#@#ice cream: #-#" +
            "#@#cold food: #-#" +
            "#@#warm food: #-#" +
            "#@#sour food: #-#" +
            "#@#sweets: #-#" +
            "#=#<font color=red>What or who do you dislike?</font>#-#" +
            "#@#a family member: #-#" +
            "#@#husband: #-#" +
            "#@#wife: #-#" +
            "#@#strangers: #-#" +
            "#@#company: #-#" +
            "#@#friends: #-#" +
            "#@#music : #-#" +
            "#@#noise: #-#" +
            "#@#sun: #-#" +
            "#@#light: #-#" +
            "#@#odors: #-#" +
            "#@#exercise: #-#" +
            "#@#being indoor: #-#" +
            "#@#closed windows: #-#" +
            "#@#tight clothes: #-#" +
            "#@#being contradicted: #-#" +
            "#@#being consoled: #-#" +
            "#@#mental work: #-#" +
            "#@#writing: #-#" +
            "#@#being wrong: #-#" +
            "#@#bathing: #-#" +
            "#=#<font color=red>What do you have a strong desire for?alcohol: #-#" +
            "#@#coffee: #-#" +
            "#@#milk: #-#" +
            "#@#bread: #-#" +
            "#@#butter:#-#" +
            "#@#cheese: #-#" +
            "#@#meat: #-#" +
            "#@#eggs: #-#" +
            "#@#chocolate: #-#" +
            "#@#lemon: #-#" +
            "#@#pickles: #-#" +
            "#@#potato: #-#" +
            "#@#sweet foods: #-#" +
            "#@#ice cream: #-#" +
            "#@#cheese: #-#" +
            "#@#pastry: #-#" +
            "#@#salty food: #-#" +
            "#@#fatty food: #-#" +
            "#@#sour food: #-#" +
            "#@#bitter food: #-#" +
            "#@#fish: #-#" +
            "#@#oysters: #-#" +
            "#@#beer: #-#" +
            "#@#wine: #-#" +
            "#@#tea: #-#" +
            "#@#pop: #-#" +
            "#@#fruits: #-#" +
            "#@#vegetables: #-#" +
            "#@#hot foods: #-#" +
            "#@#spicy foods: #-#" +
            "#@#cold foods: #-#" +
            "#@#room temperature foods: #-#" +
            "#=#<font color=red>What do you thirst for?</font>#-#" +
            "#@#large quantities of water: #-#" +
            "#@#small quantities of water: #-#" +
            "#@#hot drinks: #-#" +
            "#@#cold drinks: #-#" +
            "#@#room temperature drinks: #-#" +
            "#@#ice cold water: #-#" +
            "#@#coffee : #-#" +
            "#=#<font color=red>What do you have an addiction to?</font>#-#" +
            "#@#alcohol: #-#" +
            "#@#cigarettes/tobacco: #-#" +
            "#@#sex: #-#" +
            "#@#coffee: #-#" +
            "#@#chocolate: #-#" +
            "#@#narcotics: #-#" +
            "#@#illegal drugs: #-#" +
            "#@#sedatives: #-#" +
            "#@#diet pills :#-#" +
            "#=#<font color=red>Emotional Symptoms</font>#-#" +
            "#@#mania: #-#" +
            "#@#emotional instability: #-#" +
            "#@#hallucination: #-#" +
            "#@#confusion: #-#" +
            "#@#depression: #-#" +
            "#@#poor memory: #-#" +
            "#@#poor concentration: #-#" +
            "#@#learning disability: #-#" +
            "#@#suicidal tendencies : #-#" +
            "#@#anger: #-#" +
            "#@#broken heart: #-#" +
            "#@#anxious :#-#" +
            "#@#repulsion for sex: #-#" +
            "#@#desire for attention : #-#" +
            "#@#aversion to mental work: #-#" +
            "#@#aversion to company: #-#" +
            "#@#aversion to children : #-#" +
            "#=#<font color=red>What do you fear most?</font>#-#" +
            "#@#enclosed spaces:#-#" +
            "#@#failure: #-#" +
            "#@#affection: #-#" +
            "#@#contradiction : #-#" +
            "#@#others opinion : #-#" +
            "#@#being touched: #-#" +
            "#@#heights: #-#" +
            "#@#crowds: #-#" +
            "#@#snakes : #-#" +
            "#@#the dark: #-#" +
            "#@#driving: #-#" +
            "#@#death: #-#" +
            "#@#illness: #-#" +
            "#@#burglars: #-#" +
            "#@#thunderstorms: #-#" +
            "#@#being alone: #-#" +
            "#=#<font color=red>How would you describe your sleep?</font>#-#" +
            "#@#normal: #-#" +
            "#@#deep: #-#" +
            "#@#disturbed: #-#" +
            "#@#restless: #-#" +
            "#@#interrupted: #-#" +
            "#@#short: #-#" +
            "#@#night terrors : #-#" +
            "#@#sleep walk: #-#" +
            "#@#sleep apnea : #-#" +
            "#@#insomnia: #-#" +
            "#@#bad mood on rising : #-#" +
            "#@#avoid sleeping on the right side of the bed: #-#" +
            "#@#avoid sleeping on the left side of the bed: #-#" +
            "Do you have a recurring dream?#-#" +
            "Describe your dream: #-#" +
            "#=#<font color=red>Sexual symptoms</font> #-#" +
            "#@#excessive sexual appetite: #-#" +
            "#@#premature ejaculation: #-#" +
            "#@#pain during sexual activity: #-#" +
            "#@#problems with orgasm: #-#" +
            "#@#impotence: #-#" +
            "#@#infertility: #-#" +
            "#@#vaginal dryness: #-#" +
            "#@#sexual dysfunction: #-#" +
            "#@#difficult coition: #-#" +
            "#@#replusion for sex: #-#" +
            "#@#sexually dissatisfied: #-#" +
            "#@#painful erections: #-#" +
            "#=#<font color=blue>Physical Symptoms</font>#-#" +
            "#=#<font color=red>What is your body shape?#-#" +
            "#@#normal : #-#" +
            "#@#slender: #-#" +
            "#@#chubby: #-#" +
            "#@#overweight: #-#" +
            "#@#underweight : #-#" +
            "#@#tall stature : #-#" +
            "#@#short stature: #-#" +
            "#@#medium stature: #-#" +
            "#@#thin arms: #-#" +
            "#@#flabby upper arms: #-#" +
            "#@#thin legs: #-#" +
            "#@#flabby thighs: #-#" +
            "#@#broad shoulders: #-#" +
            "#@#wide hips: #-#" +
            "#@#narrow shoulders: #-#" +
            "#@#narrow hips: #-#" +
            "#@#flat abdomen: #-#" +
            "#@#big butt: #-#" +
            "#@#hourglass shaped body: #-#" +
            "#@#pear shaped body : #-#" +
            "#=#<font color=red>What is your face shape?#-#" +
            "#@#oval: #-#" +
            "#@#square: #-#" +
            "#@#round: #-#" +
            "#@#heart shape: #-#" +
            "#@#diamond shape: #-#" +
            "#@#high forehead: #-#" +
            "#@#receding chin: #-#" +
            "#@#square jaw: #-#" +
            "#@#big cheeks : #-#" +
            "#=#<font color=red>What is the colour of your skin?#-#" +
            "#@#white: #-#" +
            "#@#pink: #-#" +
            "#@#olive: #-#" +
            "#@#brown: #-#" +
            "#=#<font color=red>What is your facial expression?#-#" +
            "#@#happy: #-#" +
            "#@#sad: #-#" +
            "#@#fierce: #-#" +
            "#@#cold: #-#" +
            "#@#tired: #-#" +
            "#@#angry: #-#" +
            "#@#suspicious: #-#" +
            "#@#smiling: #-#" +
            "#@#proud: #-#" +
            "#@#frightened : #-#" +
            "#=#<font color=red>What condition is your skin?#-#" +
            "#@#healthy looking: #-#" +
            "#@#unhealthy looking: #-#" +
            "#@#dry: #-#" +
            "#@#oily: #-#" +
            "#@#combination: #-#" +
            "#@#acne: #-#" +
            "#@#flushed: #-#" +
            "#@#shallow: #-#" +
            "#@#liver spots: #-#" +
            "#@#freckled: #-#" +
            "#@#porcelain skin: #-#" +
            "#@#goose flesh skin: #-#" +
            "#@#rashes: #-#" +
            "#@#discoloration: #-#" +
            "#@#excessive sweating: #-#" +
            "#@#dirty looking: #-#" +
            "#@#moles: #-#" +
            "#@#itchy: #@#bruises: #-#" +
            "#@#warts: #-#" +
            "#@#hives: #-#" +
            "#@#cysts: #-#" +
            "#@#boils: #-#" +
            "#@#skin infections : #-#" +
            "#=#<font color=red>What is your natural hair colour?#-#" +
            "#@#blond: #-#" +
            "#@#red: #-#" +
            "#@#black: #-#" +
            "#@#brown: #-#" +
            "#@#grey : #-#" +
            "#@#salt and pepper : #-#" +
            "#=#<font color=red>Describe your scalp condition: #-#" +
            "#@#dry: #-#" +
            "#@#oily: #-#" +
            "#@#bald patches: #-#" +
            "#@#dandruff: #-#" +
            "#@#hair loss : #-#" +
            "#=#<font color=red>What colour are your eyes?#-#" +
            "#@#blue: #-#" +
            "#@#green: #-#" +
            "#@#hazel: #-#" +
            "#@#brown: #-#" +
            "#@#dark brown  : #-#" +
            "#=#<font color=red>Eye symptoms: #-#" +
            "#@#dry: #-#" +
            "#@#itchy: #-#" +
            "#@#burning: #-#" +
            "#@#discharges: #-#" +
            "#@#pain: #-#" +
            "#@#watery: #-#" +
            "#@#sunken: #-#" +
            "#@#contracted pupils: #-#" +
            "#@#strabismus: #-#" +
            "#@#blurred vision: #-#" +
            "#@#diplopia: #-#" +
            "#@#myopia: #-#" +
            "#@#tunnel vision: #-#" +
            "#@#photophobia: #-#" +
            "#@#swollen lids: #-#" +
            "#@#long eye lashes: #-#" +
            "#@#dark circles under the eyes: #-#" +
            "#@#tears: #-#" +
            "#@#styes: #-#" +
            "#@#sensitive: #-#" +
            "#@#redness : #-#" +
            "#=#<font color=red>Ear symptoms: #-#" +
            "#@#impaired hearing: #-#" +
            "#@#eruption behind ear: #-#" +
            "#@#itching in: #-#" +
            "#@#noises in : #-#" +
            "#@#fluids in: #-#" +
            "#@#inflammation in: #-#" +
            "#@#discharge from ears: #-#" +
            "#@#recurring ear infection: #-#" +
            "#@#pain left: #-#" +
            "#@#pain right: #-#" +
            "#@#pain behind: #-#" +
            "#@#ulceration in front of ear: #-#" +
            "#@#ringing in the ears: #-#" +
            "#@#hearing loss : #-#" +
            "#=#<font color=red>Nose symptoms: #-#" +
            "#@#long: #-#" +
            "#@#crocked: #-#" +
            "#@#cold tip: #-#" +
            "#@#red tip: #-#" +
            "#@#brown saddle on the bridge: #-#" +
            "#@#dry nostrils: #-#" +
            "#@#discharge right nostril: #-#" +
            "#@#discharge left nostril: #-#" +
            "#@#epitaxis: #-#" +
            "#@#stuffed nose: #-#" +
            "#@#dry catarrh: #-#" +
            "#@#congestion of blood: #-#" +
            "#@#obstruction of the airways: #-#" +
            "#@#polyps: #-#" +
            "#@#sensitive to odors: #-#" +
            "#@#ulcers: #-#" +
            "#@#difficulty breathing: #-#" +
            "#@#loss of smell: #-#" +
            "#@#sinus infection: #-#" +
            "#@#post-nasal drip : #-#" +
            "#=#<font color=red>Mouth symptoms: #-#" +
            "#@#dry mouth: #-#" +
            "#@#drooling: #-#" +
            "#@#bad breath: #-#" +
            "#@#oral thrash: #-#" +
            "#@#canker sore: #-#" +
            "#@#excessive salivation: #-#" +
            "#@#discoloration of the tongue: #-#" +
            "#@#protruding tongue: #-#" +
            "#@#ulcer: #-#" +
            "#@#bitter taste: #-#" +
            "#@#speech difficulty : #-#" +
            "#=#<font color=red>Lips symptoms: #-#" +
            "#@#dry: #-#" +
            "#@#cracked: #-#" +
            "#@#chapped: #-#" +
            "#@#swollen: #-#" +
            "#@#herpes : #-#" +
            "#@#Teeth symptoms: #-#" +
            "#@#asymmetric: #-#" +
            "#@#long: #-#" +
            "#@#short: #-#" +
            "#@#rectangular: #-#" +
            "#@#loose: #-#" +
            "#@#sensitive: #-#" +
            "#@#grinding: #-#" +
            "#@#numerous caries: #-#" +
            "#@#missing teeth: #-#" +
            "#@#bleeding gum: #-#" +
            "#@#gum abscess: #-#" +
            "#@#pain chewing: #-#" +
            "#@#pain drinking something cold: #-#" +
            "#@#black teeth: #-#" +
            "#@#green teeth: #-#" +
            "#@#yellow teeth : #-#" +
            "#=#<font color=red>Internal throat symptoms: #-#" +
            "#@#dry: #-#" +
            "#@#inflamed: #-#" +
            "#@#sore: #-#" +
            "#@#sensation of a lump: #-#" +
            "#@#excessive mucus: #-#" +
            "#@#narrow sensation: #-#" +
            "#@#choking sensation: #-#" +
            "#@#swallowing difficulty: #-#" +
            "#@#pain swallowing: #-#" +
            "#@#hoarseness: #-#" +
            "#@#loss of voice: #-#" +
            "#@#throat infection : #-#" +
            "#=#<font color=red>External throat symptoms: #-#" +
            "#@#induration of glands: #-#" +
            "#@#lump in the throat: #-#" +
            "#@#goitre: #-#" +
            "#@#pain of the thyroid gland : #-#" +
            "#=#<font color=red>Respiratory symptoms: #-#" +
            "#@#wheezing: #-#" +
            "#@#accelerated breathing: #-#" +
            "#@#difficult breathing: #-#" +
            "#@#deep breathing: #-#" +
            "#@#asthmatic: #-#" +
            "#@#shortness of breath: #-#" +
            "#@#obstructed respiration : #-#" +
            "#@#weak respiration: #-#" +
            "#@#gasping for air: #-#" +
            "#@#irregular respiration: #-#" +
            "#@#slow respiration: #-#" +
            "#@#loud: #-#" +
            "#@#rattling: #-#" +
            "#@#whistling: #-#" +
            "#@#asphyxia: #-#" +
            "#@#frequent respiratory infections: #-#" +
            "#@#bloody sputum: #-#" +
            "#@#yellow catarrh: #-#" +
            "#@#persistent cough : #-#" +
            "#=#<font color=red>Digestive symptoms: #-#" +
            "#@#cramps: #-#" +
            "#@#bloated: #-#" +
            "#@#heartburn: #-#" +
            "#@#feeling of emptiness: #-#" +
            "#@#fullness: #-#" +
            "#@#heaviness: #-#" +
            "#@#indigestion after a meal: #-#" +
            "#@#cutting pain: #-#" +
            "#@#gnawing pain: #-#" +
            "#@#rumbling: #-#" +
            "#@#colic: #-#" +
            "#@#flatulence: #-#" +
            "#@#stomach ulcer: #-#" +
            "#@#hiatal hernia: #-#" +
            "#@#anorexia: #-#" +
            "#@#bulimia : #-#" +
            "#=#<font color=red>Cardiovascular symptoms: #-#" +
            "#@#heart palpitations: #-#" +
            "#@#fluttering sensation: #-#" +
            "#@#constriction of the heart: #-#" +
            "#@#dilation of the heart: #-#" +
            "#@#chest pain : #-#" +
            "#=#<font color=red>Stools regular: #-#" +
            "#@#frequent: #-#" +
            "#@#hard: #-#" +
            "#@#large: #-#" +
            "#@#scanty: #-#" +
            "#@#soft: #-#" +
            "#@#dry: #-#" +
            "#@#watery: #-#" +
            "#@#bloody stools: #-#" +
            "#@#loose: #-#" +
            "#@#copious: #-#" +
            "#@#greasy: #-#" +
            "#@#frothy: #-#" +
            "#@#bilious: #-#" +
            "#@#fetid: #-#" +
            "#@#odorless: #-#" +
            "#@#colic before a stool: #-#" +
            "#@#an urge without success: #-#" +
            "#@#slimy mucous in stool: #-#" +
            "#@#involuntary stool: #-#" +
            "#@#painful stool: #-#" +
            "#@#diarrhea: #-#" +
            "#@#constipation #-#" +
            "#=#<font color=red>Shape of stool #-#" +
            "#@#s shape: #-#" +
            "#@#flat: #-#" +
            "#@#narrow: #-#" +
            "#@#chopped: #-#" +
            "#@#pasty: #-#" +
            "#@#balls like sheep dung: #-#" +
            "#=#<font color=red>Colour of stool#-#" +
            "#@#brown: #-#" +
            "#@#green: #-#" +
            "#@#grey: #-#" +
            "#@#black: #-#" +
            "#@#yellow: #-#" +
            "#@#white: #-#" +
            "#@#ash: #-#" +
            "#@#bluish: #-#" +
            "#@#orange: #-#" +
            "#=#<font color=red>Bladder symptoms: #-#" +
            "#@# weak: #-#" +
            "#@#painful: #-#" +
            "#@#obstructed: #-#" +
            "#@#calculi: #-#" +
            "#@#infection: #-#" +
            "#@#retention of urine: #-#" +
            "#@#sensation of fullness: #-#" +
            "#@#paralysis : #-#" +
            "#=#<font color=red>Urological symptoms: #-#" +
            "#@#frequent urination: #-#" +
            "#@#albuminous: #-#" +
            "#@#acrid: #-#" +
            "#@#alkaline: #-#" +
            "#@#bloody: #-#" +
            "#@#burning: #-#" +
            "#@#increased: #-#" +
            "#@#scanty: #-#" +
            "#@#involuntary: #-#" +
            "#@#urging: #-#" +
            "#@#dribbling: #-#" +
            "#@#interrupted: #-#" +
            "#@#retarded: #-#" +
            "#@#bloody urine: #-#" +
            "#@#painful urination: #-#" +
            "#@#urine comes out in drops: #-#" +
            "#@#cloudy: #-#" +
            "#@#greyish: #-#" +
            "#@#saffron colour: #-#" +
            "#@#watery: #-#" +
            "#@#prostate problems: #-#" +
            "#=#<font color=blue>Arthritic Pain</font> #-#" +
            "#=#<font color=red>Where do you feel the pain?#-#" +
            "#@#hand#-#" +
            "#@#finger#-#" +
            "#@#wrist#-#" +
            "#@#shoulder#-#" +
            "#@#thumb#-#" +
            "#@#hip#-#" +
            "#@#leg#-#" +
            "#@#calf#-#" +
            "#@#ankle#-#" +
            "#@#foot#-#" +
            "#@#toes #-#" +
            "#=#<font color=red>Where do you feel stiff?#-#" +
            "#@#shoulder : #-#" +
            "#@#hand: #-#" +
            "#@#finger : #-#" +
            "#@#hip: #-#" +
            "#@#knee: #-#" +
            "#@#ankle  : #-#" +
            "#=#<font color=red>Where do you get swollen?#-#" +
            "#@#hand: #-#" +
            "#@#wrist: #-#" +
            "#@#fingers: #-#" +
            "#@#knees: #-#" +
            "#@#legs: #-#" +
            "#@#ankles: #-#" +
            "#@#toes : #-#" +
            "#=#<font color=blue>Eczema & Psoriasis</font> #-#" +
            "#=#<font color=red>Where do you have eczema?#-#" +
            "#@#face : #-#" +
            "#@#behind the ear: #-#" +
            "#@#inside the ear: #-#" +
            "#@#neck: #-#" +
            "#@#finger : #-#" +
            "#@#hand: #-#" +
            "#@#arm: #-#" +
            "#@#leg: #-#" +
            "#@#foot: #-#" +
            "#@#genitals: #-#" +
            "#@#anus : #-#" +
            "#=#<font color=red>Where do you have psoriasis?#-#" +
            "#@#in the elbow crease : #-#" +
            "#@#in the knee crease: #-#" +
            "#@#behind the elbow: #-#" +
            "#@#on the knee : #-#" +
            "#=#<font color=red>Where do you have dry itchy skin?#-#" +
            "#@#hands : #-#" +
            "#@#feet : #-#" +
            "#@#knees: #-#" +
            "#@#elbows: #-#" +
            "#@#scalp: #-#" +
            "#@#face : #-#" +
            "#=#<font color=red>Describe your menses: #-#" +
            "#@#early: #-#" +
            "#@#late: #-#" +
            "#@#normal: #-#" +
            "#@#irregular: #-#" +
            "#@#scant: #-#" +
            "#@#heavy: #-#" +
            "#@#painful: #-#" +
            "#@#long duration: #-#" +
            "#@#short duration: #-#" +
            "#@#dysmenorrhea: #-#" +
            "#@#metorrhagia: #-#" +
            "#@#blood clots: #-#" +
            "#@#get headaches during menstruation: #-#" +
            "#=#<font color=red>What is your disposition?</font>#-#" +
            "#@#sweet: #-#" +
            "#@#affectionate : #-#" +
            "#@#shy:  #-#" +
            "#@#meticulous: #-#" +
            "#@#indifferent: #-#" +
            "#@#impulsive: #-#" +
            "#@#loquacious  : #-#" +
            "#@#jealous : #-#" +
            "#@#happy: #-#" +
            "#@#messy: #-#" +
            "#@#cheerful  : #-#" +
            "#@#capricious : #-#" +
            "#@#absent-minded : #-#" +
            "#@#quarrelsome : #-#" +
            "#@#anxious: #-#" +
            "#@#destructive: #-#" +
            "#@#easily offended : #-#" +
            "#@#generous: #-#" +
            "#@#people pleaser : #-#" +
            "#@#a bully : #-#" +
            "#@#argumentative : #-#" +
            "#@#the life of the party: #-#" +
            "#@#quick to do things: #-#" +
            "#@#vain : #-#" +
            "#@#slow learner : #-#" +
            "#@#violent: #-#" +
            "#@#ambitious : #-#" +
            "#@#obsessed with religion : #-#" +
            "#@#verbally abusive : #-#" +
            "#@#compulsive : #-#" +
            "#@#an outdoor person : #-#" +
            "#@#angry when consoled: #-#" +
            "#@#afraid of the dentist: #-#" +
            "#@#impatient : #-#" +
            "#@#philosophical: #-#" +
            "#@#afraid to die: #-#" +
            "#@#leader : #-#" +
            "#@#paradoxical: #-#" +
            "#@#lover of animals: #-#" +
            "#@#singer: #-#" +
            "#@#moved to tears from music: #-#" +
            "#@#rude: #-#" +
            "#@#relieved by crying: #-#" +
            "#@#hyper: #-#" +
            "#@#sad: #-#" +
            "#@#silly: #-#" +
            "#@#discouraged: #-#" +
            "#@#dissatisfied: #-#" +
            "#@#serious: #-#" +
            "#@#domineering: #-#" +
            "#@#fastidious : #-#" +
            "#@#dictatorial: #-#" +
            "#@#passive: #-#" +
            "#@#thirstless: #-#" +
            "#@#sensitive to noise: #-#" +
            "#@#worse in the heat: #-#" +
            "#@#worse in dry weather : #-#";
    public ImageManager imageManager;
    Super_Library_AppClass SLAc;
    ShifaDepartment ShifaDepart;
    String CurrentText = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.chat_case_open);
        imageManager = new ImageManager(this);
        SLAc = new Super_Library_AppClass(this);
        ShifaDepart = new ShifaDepartment(this);


        //final EditText edtTxtChatCaseOpen = (EditText) findViewById(R.id.edtTxtChatCaseOpen);
        //edtTxtChatCaseOpen.setText(Html.fromHtml(POST_MSG_STRING), TextView.BufferType.SPANNABLE);
        LinearLayout id_lin_chat_case = (LinearLayout) findViewById(R.id.id_lin_chat_case);
        CurrentText = "Write your problem here in brief";
        id_lin_chat_case.addView(SLAc.ChatToTemplate(CurrentText));
        SetTitleBar("Free Form - Case Discussion", "#006699");
        getWindow().setSoftInputMode(
                WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
        );

        final Button imageView1 = (Button) findViewById(R.id.tvChatPostAdv);
        final Button imageView2 = (Button) findViewById(R.id.tvChatPostBasic);
        final Button imageView3 = (Button) findViewById(R.id.tvChatPostFreeForm);
        final Button imageView4 = (Button) findViewById(R.id.tvChatPostSave);

        imageView1.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {


                //  edtTxtChatCaseOpen.setText(Html.fromHtml(POST_MSG_STRING_1), TextView.BufferType.SPANNABLE);
                LinearLayout id_lin_chat_case = (LinearLayout) findViewById(R.id.id_lin_chat_case);
                id_lin_chat_case.removeAllViews();
                CurrentText = POST_MSG_STRING_1;
                id_lin_chat_case.addView(SLAc.ChatToTemplate(CurrentText));
                SetTitleBar("Advanced Form - Case Discussion", "#880000");
                getWindow().setSoftInputMode(
                        WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
                );

            }
        });

        imageView2.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                //   edtTxtChatCaseOpen.setText(Html.fromHtml(POST_MSG_STRING), TextView.BufferType.SPANNABLE);
                LinearLayout id_lin_chat_case = (LinearLayout) findViewById(R.id.id_lin_chat_case);
                id_lin_chat_case.removeAllViews();
                CurrentText = POST_MSG_STRING;
                id_lin_chat_case.addView(SLAc.ChatToTemplate(CurrentText));
                SetTitleBar("Basic Form - Case Discussion", "#6b297f");
                getWindow().setSoftInputMode(
                        WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN
                );

            }
        });

        imageView3.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                LinearLayout id_lin_chat_case = (LinearLayout) findViewById(R.id.id_lin_chat_case);
                id_lin_chat_case.removeAllViews();

                CurrentText = "Write your problem here in brief";
                id_lin_chat_case.addView(SLAc.ChatToTemplate(CurrentText));
                SetTitleBar("Free Form - Case Discussion", "#006699");
                getWindow().setSoftInputMode(
                        WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_VISIBLE
                );

                // edtTxtChatCaseOpen.setText(Html.fromHtml(""), TextView.BufferType.SPANNABLE);
            }
        });

        imageView4.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                String ABC = ReadTemplateToChat();
                Log.e("ChatSend", ABC);
                if (ABC.equals("")) {
                    Log.e("ChatSend", "Empty Page");
                    return;
                }
                ABC = ABC.replace("Write your problem here in brief:", "");
                String ChatTextSend = ABC;// Html.toHtml(ABC);

                String ScreenMode = SLAc.GetPreferenceValue("ScreenMode");
                Log.e("ScreenMode", ScreenMode);

                SLAc.SavePreference("DiscussionTextSend", ChatTextSend);
                SLAc.SavePreference("ScreenMode", "DiscussionPostedByUser");

                String id_app = UUID.randomUUID().toString();

                ShifaDepart.PostChatSend(ChatTextSend, "-888", "", "", id_app, "", "", "");
                Intent intent = new Intent(chat_case_open.this, chat.class);
                startActivity(intent);
                finish();
            }
        });


    }

    private String ReadTemplateToChat() {
        LinearLayout id_lin_chat_case = (LinearLayout) findViewById(R.id.id_lin_chat_case);
        //int count = id_lin_chat_case.getChildCount();
        String CreatingTemplateToChat = "";
        String TextViewString = "";
        int countNumber = CurrentText.split("#-#").length;
        View v = null;
        for (int i = 0; i < countNumber; i++) {
            try {
                EditText et = (EditText) findViewById(i);
                String GetETText = et.getText().toString();
                if (!GetETText.equals("")) {
                    if (!TextViewString.equals("")) {
                        CreatingTemplateToChat += TextViewString;
                        TextViewString = "";
                    }
                    CreatingTemplateToChat += et.getHint().toString() + ":" + GetETText + "<br/>";
                    Log.e("ChAT CASE OPEN", "FOUND TT EditText" + et.getText().toString());
                }
            } catch (Exception ex) {
                try {
                    CheckBox ck = (CheckBox) findViewById(i);
                    if (ck.isChecked() == true) {
                        if (!TextViewString.equals("")) {
                            CreatingTemplateToChat += TextViewString;
                            TextViewString = "";
                        }
                        CreatingTemplateToChat += ck.getText().toString() + ", ";
                        Log.e("ChAT CASE OPEN", "FOUND TT CheckBox" + ck.isChecked() + ck.getText().toString());
                    }
                } catch (Exception ex1) {
                    try {
                        TextView tt = (TextView) findViewById(i);
                        String HtmlString = "<font color =blue>" + tt.getText().toString() + "</font>";
                        TextViewString = "<br/>" + HtmlString + "<br/>";
                        Log.e("ChAT CASE OPEN", "FOUND TT TextView" + tt.getText().toString());
                    } catch (Exception ex2) {
                        Log.e("ChAT CASE OPEN", "ERROR NOT FOUND TT " + i);

                    }
                }
            }
        }


        return CreatingTemplateToChat;
    }

    @Override
    protected void onStop() {
        super.onStop();


    }

    private void SetTitleBar(String Caption, String ColorCode) {
        try {
            setTitle(Caption);
            ActionBar bar = getActionBar();
            bar.setBackgroundDrawable(new ColorDrawable((Color.parseColor(ColorCode))));

            getActionBar().setIcon(R.drawable.ic_action_action_assignment_turned_in);
        } catch (Exception e) {
            Log.e("ChatCaseOpen", "Error in SetTtitleBar " + e.toString());
        }
    }

}
