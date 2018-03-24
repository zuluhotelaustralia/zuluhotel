(define-module (uo writer)
  #:use-module (ice-9 binary-ports)
  #:use-module (rnrs bytevectors))

(define-public (write-s32 port n)
  (let ((bv (make-bytevector 4)))
    (bytevector-s32-set! bv 0 n (endianness little))
    (put-bytevector port bv)))

(define-public (write-s16 port n)
  (let ((bv (make-bytevector 2)))
    (bytevector-s16-set! bv 0 n (endianness little))
    (put-bytevector port bv)))

(define-public write-u8 put-u8)

(define-public (write-string port s)
  (let ((bv (string->utf8 s)))
    (write-s16 port (bytevector-length bv))
    (put-bytevector port (string->utf8 s))))
