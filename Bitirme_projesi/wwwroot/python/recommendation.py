import pandas as pd
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity
import nltk
from nltk.stem.porter import PorterStemmer
import spotipy
from spotipy.oauth2 import SpotifyClientCredentials
import sys
import json

def recommend(song_name):
    # Seçilen şarkıya göre önerileri al ve ekrana yazdır
    idx = df[df['song'] == song_name].index[0]
    distances = sorted(list(enumerate(cosine_similarity_matrix[idx])), reverse=True, key=lambda x: x[1])

    recommended_songs = []
    for m_id in distances[1:6]:  # İlk 5 benzer şarkıyı al
        recommended_songs.append({"song": df.iloc[m_id[0]]['song'], "singer": df.iloc[m_id[0]]['singer']})

    # Spotify API kullanarak albüm kapaklarını getir
    for song in recommended_songs:
        song['album_cover'] = get_album_cover_url(song['song'], song['singer'])

    # JSON formatında döndürme
    with open('recommended_songs.json', 'w') as json_file:
        json.dump(recommended_songs, json_file)

    return recommended_songs

def get_album_cover_url(song_name, artist_name):
    CLIENT_ID = "9ffd903cac69483493e0e7e091fad133"
    CLIENT_SECRET = "43a99f4595e548b58e079fd6ec1ed367"

    # Spotify API'ya erişim için kimlik doğrulamasını yap
    client_credentials_manager = SpotifyClientCredentials(client_id=CLIENT_ID, client_secret=CLIENT_SECRET)
    sp = spotipy.Spotify(client_credentials_manager=client_credentials_manager)

    # Şarkı adı ve sanatçı adına göre şarkıyı bul
    search_query = f"track:{song_name} artist:{artist_name}"
    results = sp.search(q=search_query, type="track")

    if results and results["tracks"]["items"]:
        track = results["tracks"]["items"][0]
        album_cover_url = track["album"]["images"][0]["url"]
        return album_cover_url
    else:
        return "https://i.postimg.cc/0QNxYz4V/social.png"

# CSV dosyasını oku
df = pd.read_csv("C:\\Users\\ASUS\\source\\repos\\Bitirme_projesi\\Bitirme_projesi\\wwwroot\\python\\turkish_song_lyrics.csv")

# Text Cleaning/Text Preprocessing
df['lyrics'] = df['lyrics'].str.lower().replace(r'^\w\s', ' ').replace(r'\n', ' ', regex=True)

stemmer = PorterStemmer()
def tokenization(txt):
    tokens = nltk.word_tokenize(txt)
    stemming = [stemmer.stem(w) for w in tokens]
    return " ".join(stemming)

df['lyrics'] = df['lyrics'].apply(lambda x: tokenization(x))

# TF-IDF Vectorization
tfidf_vectorizer = TfidfVectorizer(analyzer='word', stop_words='english')
tfidf_matrix = tfidf_vectorizer.fit_transform(df['lyrics'])
cosine_similarity_matrix = cosine_similarity(tfidf_matrix)

# Seçilen şarkıya göre önerileri al ve ekrana yazdır
if __name__ == "__main__":
    if len(sys.argv) > 1:
        selected_song = sys.argv[1]
        recommendations = recommend(selected_song)
        print(recommendations)
