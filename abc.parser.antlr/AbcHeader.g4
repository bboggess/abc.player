grammar AbcHeader ;

tuneHeader : fieldNumber fieldTitle optionalField* fieldKey ;

fieldNumber : 'X:' INT NEWLINE ;
fieldTitle : 'T:' text NEWLINE ;
fieldKey : 'K:' keySignature NEWLINE ;

optionalField : fieldComposer | fieldLength | fieldMeter | fieldTempo ;
fieldComposer : 'C:' text NEWLINE ;
fieldLength : 'L:' fraction NEWLINE ;
fieldMeter : 'M:' timeSignature NEWLINE ;
fieldTempo : 'Q:' tempoDef NEWLINE ;

keySignature : note (accidentalKey)? (modeKey)? ;
accidentalKey : '#' | 'b' ;
modeKey : 'm' ;

timeSignature : fraction | 'C' | 'C|' ;
fraction : INT '/' INT ;
tempoDef : fraction '=' INT ;

note : 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

text : (~NEWLINE)+ ;

INT : [1-9] [0-9]* ;
NEWLINE : '\r'? '\n' | '\r' ;
ANY : . ;