grammar ABC ;
options { language=CSharp; }

abc_file : abc_header abc_music ;

abc_header : field_number field_title optional_field* field_key ;

field_number : 'X:' INT NEWLINE ;
field_title : 'T:' text NEWLINE ;
field_key : 'K:' key_signature NEWLINE ;

optional_field : field_composer | field_length | field_meter | field_tempo ;
field_composer : 'C:' text NEWLINE ;
field_length : 'L:' fraction NEWLINE ;
field_meter : 'M:' time_signature NEWLINE ;
field_tempo : 'Q:' tempo_def NEWLINE ;

key_signature : note (accidental_key)? (mode_key)? ;
accidental_key : '#' | 'b' ;
mode_key : 'm' ;

time_signature : fraction | 'C' | 'C|' ;
fraction : INT '/' INT ;
tempo_def : fraction '=' INT ;

note : 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' ;

abc_music : additional_line* ;

additional_line : text+ NEWLINE ;
text : (~NEWLINE)+ ;

INT : [1-9] [0-9]* ;
NEWLINE : '\r'? '\n' | '\r' ;
ANY : . ;