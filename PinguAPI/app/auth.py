from fastapi import Header
from firebase_admin import auth
from typing import Optional, Dict, Any

def get_actual_user(authorization: Optional[str] = Header(None)) -> Optional[Dict[str, Any]]:

    if not authorization or not authorization.startswith("Bearer "): return None

    token = authorization.split("Bearer ")[1]

    try:
        decoded_user = auth.verify_id_token(token)
        return decoded_user
    except Exception as e:
        # TODO: This would be better as a log instead of a print
        print(f"Unauthorized Access or Invalid Token: {e}")
        return None