grammar AbcBody ;

tuneBody : baseNote* ;

baseNote : 'a' | 'b' | 'c' | 'd' | 'e' | 'f' | 'g'
	| 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

NEWLINE : '\r'? '\n' | '\r' ;
ANY : . ;