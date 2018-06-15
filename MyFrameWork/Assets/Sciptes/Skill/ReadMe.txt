所有技能上挂载相应的释放器 Deployer

SkillDeployer 抽象技能释放器
对 CurrentSkillData 属性赋值时初始化伤害 选区算法脚本
供子类使用的方法：	CollectSkill	技能回收 每次使用前进行调用
			CalculateTargets  技能释放前调用 查找工具目标
需子类重写的方法：DeployerSkill 技能释放的方法

ActorSkillManager 单个角色身上的技能管理类
脚本启动时初始化所有技能，可通过在界面手动配置或者读取配置表进行配置，先通过对象池创建进行缓存
内置单个技能管理：如剩余冷却时间获取，生成技能

能释放技能的物体上挂载 ActorSkillSystem 依赖 ActorSkillManager
ActorSkillSystem 技能系统外观类 向外部提供释放技能接口、随机释放技能等 结合动画控制器播放动画

动画片段上手动添加动画事件 Animator组件同级挂载Animation Event Behaviour 执行动画片段添加的事件