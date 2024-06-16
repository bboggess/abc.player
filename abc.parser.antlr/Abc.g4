grammar Abc ;
options { language=CSharp; }

/**
 * Overarching grammmar for the entire file.
 * A subset of https://abcnotation.com/wiki/abc:standard:v2.1
 */
abcTune : tuneHeader tuneBody ;

/**
 * Header rules begin here
 */
tuneHeader : fieldNumber fieldTitle optionalField* fieldKey ;

fieldNumber : 'X:' INT NEWLINE ;
fieldTitle : 'T:' text NEWLINE ;
fieldKey : 'K:' keySignature NEWLINE ;

optionalField : fieldComposer | fieldLength | fieldMeter | fieldTempo ;
fieldComposer : 'C:' text NEWLINE ;
fieldLength : 'L:' fraction NEWLINE ;
fieldMeter : 'M:' timeSignature NEWLINE ;
fieldTempo : 'Q:' tempoDef NEWLINE ;

keySignature : keyNote (accidentalKey)? (modeKey)? ;
accidentalKey : '#'		# Sharp
				| 'b'	# Flat
				;
modeKey : 'm' # Minor
	;

timeSignature : fraction # fractionMeter
				| 'C'	 # commonTime
				| 'C|'	 # cutTime
				;
fraction : INT '/' INT ;
tempoDef : fraction '=' INT ;

keyNote : 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

text : (~NEWLINE)+ ;

/**
 * Body rules begin here
 */
tuneBody : element* ;

element : note | WS ;

note : pitchOrRest noteLength ;

pitchOrRest : pitch | rest ;
rest : 'z' ;
pitch : accidental? baseNote octave? ;

noteLength : INT? ('/' INT?)? ;

accidental : '^' | '^^' | '=' | '_' | '__' ;
octave : '\''+ | ','+ ;
baseNote : 'a' | 'b' | 'c' | 'd' | 'e' | 'f' | 'g'
	| 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

/**
 * Generic fields, tokens, etc.
 */
INT : [1-9] [0-9]* ;
NEWLINE : '\r'? '\n' | '\r' ;
WS : [ \t\r\n]  ;
ANY : . ;