grammar AbcBody ;

tuneBody : note* ;

note : pitchOrRest noteLength ;

pitchOrRest : pitch | rest ;
rest : 'z' ;
pitch : accidental? baseNote octave? ;

noteLength : INT? ('/' INT?)? ;

accidental : '^' | '^^' | '=' | '_' | '__' ;
octave : '\''+ | ','+ ;
baseNote : 'a' | 'b' | 'c' | 'd' | 'e' | 'f' | 'g'
	| 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

INT : [1-9] [0-9]* ;
NEWLINE : '\r'? '\n' | '\r' ;