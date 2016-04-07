from Crypto.Hash import SHA256

def Hash_Password(password):
    hash = SHA256.new()
    hash.update(password)
    hashed_password = hash.hexdigest()
    return hashed_password



