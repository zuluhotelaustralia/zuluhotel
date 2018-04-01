(define-module (uo reader)
  #:use-module (ice-9 binary-ports)
  #:use-module (rnrs bytevectors))

(define-public (read-n port n)
  (let ((data (get-bytevector-n port n)))
    (or (= (bytevector-length data) n)
        (throw 'unexpected-eof))
    data))

(define-public (read-u8 port)
  (let ((u8 (get-u8 port)))
    (if (eof-object? u8)
        (throw 'unexpected-eof)
        u8)))

(define-public (read-s32 port)
  (bytevector-s32-ref (read-n port 4) 0 (endianness little)))

(define-public (read-s16 port)
  (bytevector-s16-ref (read-n port 2) 0 (endianness little)))

(define-public (read-string port)
  (let ((length (read-s16 port)))
    (utf8->string (read-n port length))))
