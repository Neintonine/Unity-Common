using UnityEngine;

namespace Common.Runtime.Animation
{
    [AddComponentMenu("Animation/Animator Arguments - Transform")]
    [RequireComponent(typeof(AnimatorArguments))]
    public sealed class AnimatorArgumentsTransform : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        [SerializeField] private bool _addPosition;
        [SerializeField] private bool _useWorldPosition;
        [SerializeField] private string _positionName;
        
        [SerializeField] private bool _addRotation;
        [SerializeField] private bool _useEuler;
        [SerializeField] private bool _useWorldRotation;
        [SerializeField] private string _rotationName;
        
        [SerializeField] private bool _addScale;
        [SerializeField] private string _scaleName;

        [SerializeField] private bool _addForward;
        [SerializeField] private string _forwardName;
        
        [SerializeField] private bool _addRight;
        [SerializeField] private string _rightName;

        [SerializeField] private bool _addUp;
        [SerializeField] private string _upName;

        private AnimatorArguments _arguments;

        private void Awake()
        {
            this._arguments = this.GetComponent<AnimatorArguments>();
            if (this._transform)
            {
                return;
            }

            Debug.LogError("Setting Transformation values to animator arguments failed: Transform is not set.", this);
            this.enabled = false;
        }

        private void Start()
        {
            this.SetPosition();
            this.SetScale();
            this.SetRotation();

            this.SetForward();
            this.SetRight();
            this.SetUp();
        }

        private void FixedUpdate()
        {
            this.SetPosition();
            this.SetScale();
            this.SetRotation();

            this.SetForward();
            this.SetRight();
            this.SetUp();
        }

        private void SetForward()
        {
            if (!this._addForward)
            {
                return;
            }

            this._arguments.Set(this._forwardName, this._transform.forward);
        }
        
        private void SetRight()
        {
            if (!this._addRight)
            {
                return;
            }

            this._arguments.Set(this._rightName, this._transform.right);
        }
        
        private void SetUp()
        {
            if (!this._addUp)
            {
                return;
            }

            this._arguments.Set(this._upName, this._transform.up);
        }

        private void SetRotation()
        {
            if (!this._addRotation)
            {
                return;
            }

            if (this._useEuler)
            {
                Vector3 eulerAngles = this._useWorldRotation ? this._transform.eulerAngles : this._transform.localEulerAngles;
                this._arguments.Set(this._rotationName, eulerAngles);
                return;
            }
            
            Quaternion rotation = this._useWorldRotation ? this._transform.rotation : this._transform.localRotation;
            this._arguments.Set(this._rotationName, rotation);
        }

        private void SetScale()
        {
            if (!this._addScale)
            {
                return;
            }
            this._arguments.Set(this._scaleName, this._transform.localScale);
        }

        private void SetPosition()
        {
            if (!this._addPosition)
            {
                return;
            }

            Vector3 pos = this._useWorldPosition ? this._transform.position : this._transform.localPosition;
            this._arguments.Set(this._positionName, pos);
        }
    }
}