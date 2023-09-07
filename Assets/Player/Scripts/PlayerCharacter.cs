using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(ShootAtCameraLook))]
[RequireComponent(typeof(Health))]

public class PlayerCharacter : BaseCharacter
{
    public static string PLAYER_TAG = "Player";

    [SerializeField] int _damagePerHit = 1;
    private PlayerHUD _hud;
    private ShootAtCameraLook _shootAtCameraLook;
    private MeleeAttack _meleeAttack;
    private Movement _movement;
    private Health _health;
    public BasePlayerClass _class = null;

    public bool _isMelee = true;
    public float _baseAttackSpeed = 1f;
    public float _attackSpeed = 1f;

    //talents
    private BaseTalentTree _talentTree;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        base.Awake();
        _shootAtCameraLook = GetComponent<ShootAtCameraLook>();
        _shootAtCameraLook.SetPlayer(this);
        _movement = GetComponent<Movement>();
        _meleeAttack = GetComponentInChildren<MeleeAttack>();
        _meleeAttack.SetPlayer(this);
        _health = GetComponentInChildren<Health>();
        _hud = GetComponentInChildren<PlayerHUD>();
        _shootAtCameraLook.SetDamage(_damagePerHit);

        if (_class)
        {
            _class.Initialize(this);

            // bad but for prototype level
            // REMOVE THIS
            foreach(ClassSelection obj in FindObjectsOfType<ClassSelection>())
            {
                Destroy(obj.gameObject);
            }
        }
    }

    private void Start()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _health.Heal(_health.GetMaxHealth());
    }

    public void OnOffensiveSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_offensiveSkill)
                _offensiveSkill.Activate(this);
        }
    }

    public void OnDefensiveSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_defensiveSkill)
                _defensiveSkill.Activate(this);
        }
    }

    public void OnUtilitySkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_utilitySkill)
                _utilitySkill.Activate(this);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_isMelee && _meleeAttack)
        {
            _meleeAttack.OnShoot(context);
        }
        else if(!_isMelee && _shootAtCameraLook)
        {
            _shootAtCameraLook.OnShoot(context);
        }
    }

    public void AddItem(BaseItem newItem)
    {
        bool isDeleted = false;
        foreach(BaseItem item in _items)
        {
            if(newItem.name == item.name)
            {
                item.AddCount(1);
                Destroy(newItem.gameObject, 0.01f);
                _hud.UpdateItems();
                item.UpdateValues(this);
                isDeleted = true;
                break;
            }
        }

        if (!isDeleted)
        {
            newItem.gameObject.transform.parent = transform;
            newItem.transform.localPosition = Vector3.zero;
            _items.Add(newItem);
            newItem.OnPickup(this);
            _hud.UpdateItems();
        }
    }

    public void TriggerOnAttack()
    {
        foreach(BaseItem item in _items)
        {
            item.OnAttack(this);
        }
    }

    public void ResetAttackSpeed()
    {
        _attackSpeed = _baseAttackSpeed;
        _meleeAttack.SetAttackSpeed(_baseAttackSpeed);
        _shootAtCameraLook.SetAttackSpeed(_baseAttackSpeed);
    }

    //getters
    public ShootAtCameraLook GetShootCameraLook()
    {
        return _shootAtCameraLook;
    }

    public float GetBaseAttackSpeed()
    {
        return _baseAttackSpeed;
    }

    public float GetAttackSpeed()
    {
        return _attackSpeed;
    }

    public MeleeAttack GetMeleeAttack()
    { 
        return _meleeAttack; 
    }

    public Movement GetMovement()
    {
        return _movement;
    }

    public Camera GetCamera()
    {
        return _shootAtCameraLook.GetCamera();
    }

    public Health GetHealth()
    {
        return _health;
    }

    public List<BaseItem> GetItems()
    {
        return _items;
    }

    //setters
    public void SetClass(BasePlayerClass playerClass)
    {
        _class = playerClass;
        _class.Initialize(this);
        _talentTree.Initialize(this);
    }

    public void SetTalentTree(BaseTalentTree tree)
    {
        _talentTree = tree;
        if (!_talentTree) return;
        //tree.Initialize(this);
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        _attackSpeed = attackSpeed;
        _meleeAttack.SetAttackSpeed(_attackSpeed);
        _shootAtCameraLook.SetAttackSpeed(_attackSpeed);
    }

    public float IncreaseAttackSpeed(float attackSpeedBuff, bool isPercentage = true)
    {
        if (isPercentage)
        {
            _attackSpeed *= attackSpeedBuff;
        }
        else
        {
            _attackSpeed -= attackSpeedBuff;
            if(_attackSpeed < 0 ) _attackSpeed = 0f;
        }
        _meleeAttack.SetAttackSpeed(_attackSpeed);
        _shootAtCameraLook.SetAttackSpeed(_attackSpeed);

        return _attackSpeed;
    }
}
