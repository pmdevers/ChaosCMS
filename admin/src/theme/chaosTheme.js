import { 
  cyan700,
  grey600, grey800,
  pinkA100, pinkA200, pinkA400,
  fullWhite, darkBlack
} 
from 'material-ui/styles/colors';
import {fade} from 'material-ui/utils/colorManipulator';
import spacing from 'material-ui/styles/spacing';

export default {
  spacing: spacing,
  fontFamily: 'Roboto, sans-serif',
  borderRadius: 2,
  palette: {
    primary1Color: darkBlack,
    primary2Color: darkBlack,
    primary3Color: grey600,
    accent1Color: grey600,
    accent2Color: grey800,
    accent3Color: pinkA100,
    textColor: fullWhite,
    secondaryTextColor: fade(fullWhite, 0.7),
    alternateTextColor: fullWhite, //'#303030',
    canvasColor: '#303030',
    borderColor: fade(fullWhite, 0.3),
    disabledColor: fade(fullWhite, 0.3),
    pickerHeaderColor: fade(fullWhite, 0.12),
    clockCircleColor: fade(fullWhite, 0.12),
  },
};