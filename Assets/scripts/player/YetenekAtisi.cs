using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class YetenekAtisi : MonoBehaviour
{   
    [SerializeField] private Transform atisNoktasi;
    [SerializeField] private Transform Dronekonum;
    private Rigidbody2D playerBody;

    //en yakýn duþmaný bulma
    List<GameObject> dusmanlar = new List<GameObject>();
    private int random;
    private Transform hedef;

    //Yetenek seviyeleri
    private int FireBallSeviyesi = 0;
    private int LightningSeviyesi = 0;
    private int Ice_SharpSeviyesi = 0;

    private int KaranliginZirvesiSeviyesi = 0;
    private int BicCekSeviyesi = 0;
    private int KunaiSeviyesi = 0;
    private int ShurikenSeviyesi = 0;
    private int GizemliAtisSeviyesi = 0;
    private int DroneSeviyesi = 0;
    private int OkSeviyesi = 1;

    private int KaranliginZirvesiOncekiSeviyesi = 0;

    //yeteneklerin süresini belirten deðiþkenler
    private float FireBallSuresi = 0f;
    private float LightningSuresi = 0f;
    private bool KaranliginZirvesiAcikmi = false;
    private float KunaiSuresi = 0f;
    private float ShurikenSuresi = 0f;
    private float BicCekSuresi = 0f;
    private float Ice_SharpSuresi = 0f;
    private float GizemliAtisSuresi = 0f;
    private bool DroneVarmi = false;
    private float OkSuresi = 0f;

    //þablonlar
    [SerializeField] private GameObject FireBallSablonu;
    [SerializeField] private GameObject LightningSablonu;
    [SerializeField] private GameObject KaranliginZirvesiSablonu;
    [SerializeField] private GameObject KunaiSablonu;
    [SerializeField] private GameObject ShurikenSablonu;
    [SerializeField] private GameObject BicCekSablonu;
    [SerializeField] private GameObject Ice_SharpSablonu;
    [SerializeField] private GameObject GizemliAtisSablonu;
    [SerializeField] private GameObject DroneSablonu;
    [SerializeField] private GameObject OkSablonu;

    //class objeleri
    private KaranliginZirvesi karanliginZirvesi;
    private Drone drone;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DusmaniSec", 0f, 0.5f);
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        yetenekAtisi();
    }

    void DusmaniSec()
    {
        GameObject[] dusmanlarArray = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> dusmanlar = new List<GameObject>();

        for (int i = 0; i < dusmanlarArray.Length; i++)
        {
            float dusmanaUzaklik = Vector3.Distance(transform.position, dusmanlarArray[i].transform.position);

            if (dusmanaUzaklik < 10)
            {
                dusmanlar.Add(dusmanlarArray[i]);
            }

        }

        if(dusmanlar.Count > 0)
        {
            GameObject rastgeleDusman = null;
            hedef = null;

            random = Random.Range(0, dusmanlar.Count);
            rastgeleDusman = dusmanlar[random];

            hedef = rastgeleDusman.transform;

            if (rastgeleDusman == null)
            {
                hedef = null;
            }
        }
    }

    void yetenekAtisi()
    {
        //elementlerin güçleri
        if (FireBallSeviyesi > 0) FireBall();
        else if (LightningSeviyesi > 0) Lightning();
        else if(Ice_SharpSeviyesi > 0) Ice_Sharp();

        //yetenek atýþlarý
        if (KaranliginZirvesiSeviyesi > 0) KaranliginZirvesi();

        if (BicCekSeviyesi > 0) BicCek();
        if (KunaiSeviyesi > 0) Kunai();
        if (ShurikenSeviyesi > 0) Shuriken();

        if (BicCekSeviyesi > 0) BicCekAc();

        if(GizemliAtisSeviyesi > 0) GizemliAtis();

        if(DroneSeviyesi> 0) Drone();

        //if (OkSeviyesi > 0) Ok(); //yapýmda

    }

    void FireBall()
    {
        if (FireBallSuresi <= 0f)
        {
            DusmaniSec();
            FireBallSuresi = 3f;

            GameObject Fireball = Instantiate(FireBallSablonu, atisNoktasi.position, atisNoktasi.rotation);
            FireBall fireball_1 = Fireball.GetComponent<FireBall>();

            if (fireball_1 != null)
            {
                fireball_1.HedefAta(hedef);
                fireball_1.setSeviye(FireBallSeviyesi);
            }
        }
        else FireBallSuresi -= Time.deltaTime;
    }

    void Lightning()
    {
        if (LightningSuresi <= 0f)
        {
            DusmaniSec();
            LightningSuresi = 2f;

            GameObject Lightning = Instantiate(LightningSablonu, atisNoktasi.position, atisNoktasi.rotation);
            Lightning lightning_1 = Lightning.GetComponent<Lightning>();

            if (lightning_1 != null)
            {
                lightning_1.HedefAta(hedef);
                lightning_1.setSeviye(LightningSeviyesi);
            }
        }
        else LightningSuresi -= Time.deltaTime;
    }

    void KaranliginZirvesi()
    {

        if (!KaranliginZirvesiAcikmi)
        {
            GameObject KaranliginZirvesi = Instantiate(KaranliginZirvesiSablonu, atisNoktasi.position, atisNoktasi.rotation,atisNoktasi);
            karanliginZirvesi = KaranliginZirvesi.GetComponent<KaranliginZirvesi>();

            if (karanliginZirvesi != null)
            {
                KaranliginZirvesiAcikmi = true;
            } 
        }
        else
        {
            if(KaranliginZirvesiOncekiSeviyesi < KaranliginZirvesiSeviyesi)
            {
                karanliginZirvesi.setSeviye(KaranliginZirvesiSeviyesi);
                KaranliginZirvesiOncekiSeviyesi = KaranliginZirvesiSeviyesi;
            }       
        }
    }

    void Kunai()
    {
        if (KunaiSuresi <= 0f)
        {
            DusmaniSec();
            KunaiSuresi = 2f;

            GameObject Kunai = Instantiate(KunaiSablonu, atisNoktasi.position, atisNoktasi.rotation);
            Kunai kunai = Kunai.GetComponent<Kunai>();

            if (kunai != null)
            {
                kunai.HedefAta(hedef);
                kunai.setSeviye(KunaiSeviyesi);
            }
        }
        else KunaiSuresi -= Time.deltaTime;
    }

    void Shuriken()
    {
        if (ShurikenSuresi <= 0)
        {
            DusmaniSec();
            ShurikenSuresi = 3f;

            GameObject Shuriken = Instantiate(ShurikenSablonu, atisNoktasi.position, atisNoktasi.rotation);
            Shuriken shuriken = Shuriken.GetComponent<Shuriken>();

            if(shuriken != null)
            {
                shuriken.HedefAta(hedef);
                shuriken.setSeviye(ShurikenSeviyesi);
            }
        }
        else ShurikenSuresi -= Time.deltaTime;
    }

    void BicCek()
    {
        
        if (BicCekSuresi <= 0f)
        {
            DusmaniSec();
            BicCekSuresi = 1f;

            GameObject BicCek = Instantiate(BicCekSablonu, atisNoktasi.position, atisNoktasi.rotation);
            BicCek biccek = BicCek.GetComponent<BicCek>();

            if (biccek != null)
            {
                biccek.HedefAta(hedef);
                biccek.SetMerkez(atisNoktasi);
                biccek.setSeviye(BicCekSeviyesi);
            }
        }
        else BicCekSuresi -= Time.deltaTime;
    }

    void Ice_Sharp()
    {
        if (Ice_SharpSuresi <= 0)
        {
            Ice_SharpSuresi = 4f;

            GameObject Ice = Instantiate(Ice_SharpSablonu, atisNoktasi.position, atisNoktasi.rotation);
            Ice_Sharp ice = Ice.GetComponent<Ice_Sharp>();
            ice.setSeviye(Ice_SharpSeviyesi);
        }
        else Ice_SharpSuresi -= Time.deltaTime;
    }

    void GizemliAtis()
    {
        if (GizemliAtisSuresi <= 0)
        {
            DusmaniSec();
            GizemliAtisSuresi = 0.8f;

            GameObject GizemliAtis = Instantiate(GizemliAtisSablonu, atisNoktasi.position, atisNoktasi.rotation);
            GizemliAtis gizemliAtis = GizemliAtis.GetComponent<GizemliAtis>();

            if(gizemliAtis != null)
            {
                gizemliAtis.HedefAta(hedef.position);
                gizemliAtis.setSeviye(GizemliAtisSeviyesi);
            }
        }
        else GizemliAtisSuresi -= Time.deltaTime;
    }

    void Drone()
    {

        if (!DroneVarmi)
        {
            DroneVarmi = true;

            GameObject Drone = Instantiate(DroneSablonu, transform.position + new Vector3(-2,2,0), transform.rotation);
            drone = Drone.GetComponent<Drone>();

            if(drone != null)
            {
                drone.SetMerkez(Dronekonum);
            }
        }
        else
        {
            drone.setSeviye(DroneSeviyesi);
        }
    }

    void Ok()
    {
        if (OkSuresi <= 0)
        {
            OkSuresi = 2f;

            Vector3 Yon = transform.position;
            bool yon = gameObject.GetComponent<player>().getYon();

            if (yon) Yon += new Vector3(10, 0, 0);
            else Yon += new Vector3(-10, 0, 0);

            GameObject Ok = Instantiate(OkSablonu, transform.position, transform.rotation);
            ok OK = Ok.GetComponent<ok>();

            if (OK != null)
            {
                OK.setHedef(Yon);
                OK.setSeviye(OkSeviyesi);
            }
        }
        else OkSuresi -= Time.deltaTime;
    }

    public void BicCekAc()
    {
        KunaiSeviyesi = 0;
        ShurikenSeviyesi = 0;
    }

    public void setFireBallSeviyesi()
    {
        this.FireBallSeviyesi++;
    }

    public void setLightningSeviyesi()
    {
        this.LightningSeviyesi++;
    }

    public void setIce_SharpSeviyesi()
    {
        this.Ice_SharpSeviyesi++;
    }

    public void setBicCekSeviyesi()
    {
        this.BicCekSeviyesi++;
    }

    public void setDroneSeviyesi()
    {
        this.DroneSeviyesi++;
    }

    public void setGizemliAtisSeviyesi()
    {
        this.GizemliAtisSeviyesi++;
    }

    public void setKaranliginZirvesiSeviyesi()
    {
        this.KaranliginZirvesiSeviyesi++;
    }

    public void setKunaiSeviyesi()
    {
        this.KunaiSeviyesi++;
    }

    public void setShurikenSeviyesi()
    {
        this.ShurikenSeviyesi++;
    }

    public void setOkSeviyesi()
    {
        this.OkSeviyesi++;
    }

    public int getBicCekSeviyesi()
    {
        return this.BicCekSeviyesi;
    }
    public int getKunaiSeviyesi()
    {
        return this.KunaiSeviyesi;
    }
    public int getShurikenSeviyesi()
    {
        return this.ShurikenSeviyesi;
    }
    public int getKaranliginZirvesiSeviyesi()
    {
        return this.KaranliginZirvesiSeviyesi;
    }
    public int getDroneSeviyesi()
    {
        return this.DroneSeviyesi;
    }
    public int getGizemliAtisSeviyesi()
    {
        return this.GizemliAtisSeviyesi;
    }

    public int getOkSeviyesi()
    {
        return this.OkSeviyesi;
    }
}
