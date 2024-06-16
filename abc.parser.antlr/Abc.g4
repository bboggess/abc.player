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

fieldNumber : 'X:' WS* INT NEWLINE ;
fieldTitle : 'T:' WS* text NEWLINE ;
fieldKey : 'K:' WS* keySignature NEWLINE ;

optionalField : fieldComposer | fieldLength | fieldMeter | fieldTempo ;
fieldComposer : 'C:' WS* text NEWLINE ;
fieldLength : 'L:' WS* fraction NEWLINE ;
fieldMeter : 'M:' WS* timeSignature NEWLINE ;
fieldTempo : 'Q:' WS* tempoDef NEWLINE ;

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

element : note | WS | NEWLINE ;

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
WS : [ \t] ;
NEWLINE : '\r'? '\n' | '\r' ;
ANY : . ;