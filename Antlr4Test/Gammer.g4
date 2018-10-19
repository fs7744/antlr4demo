grammar Gammer;

moduleDefinition
    : 'module' moduleName '{' memberDefinition* '}' ';'*
    ;

memberDefinition
    : enumDefinition
    | structDefinition
    ;

moduleName
    : ID
    | moduleName '.' ID
    ;

structDefinition
    : 'struct' ID '{' fieldDefinition*  '}' ';'*
    ;

fieldDefinition
    : Int fieldOption typeDeclaration ID fieldValue* ';'
    ;

fieldOption: 'require' | 'optional';

fieldValue
    : '=' Int
    | '=' Float
    |  '=' String
    ;

typeDeclaration
    : 'short'
    | 'byte'
    | 'int'
    | 'string'
    | 'vector' '<'typeDeclaration '>'
    | 'map' '<'typeDeclaration ',' typeDeclaration '>'
    ;

enumDefinition: 'enum' ID '{' enumDeclaration*  '}' ';'*;

enumDeclaration: ID fieldValue* ','*;

//------ Identifiers
ID : ID_Letter (ID_Letter | Digit)* ;
fragment ID_Letter : 'a'..'z' | 'A'..'Z' | '_' ;
fragment Digit : '0'..'9';

//------ Numbers
Int   : Digit+ ;
Float : Digit+ '.' Digit*
      | '.' Digit+
      ;

//------ Strings
String : '"' (ESC | .)*? '"' ;
fragment ESC : '\\' [btnr"\\] ;  // \b, \t, \n, ...

LineComment: '//' .*? '\n' -> skip;
BlockComment : '/*' .*? '*/' -> skip;

WS: [ \t\n\r]+ -> skip;